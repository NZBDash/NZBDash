(function ($) {
    var plugin = GridMvc.prototype;

    // store copies of the original plugin functions before overwriting
    var functions = {};
    for (var i in plugin) {
        if (typeof (plugin[i]) === 'function') {
            functions[i] = plugin[i];
        }
    }


    // extend existing functionality of the gridmvc plugin
    $.extend(true, plugin, {
        parseFilterValues: function (filterData) {
            var opt = $.parseJSON(filterData);
            var filters = [];
            for (var i = 0; i < opt.length; i++) {
                filters.push({ filterValue: opt[i].FilterValue, filterType: opt[i].FilterType, columnName: opt[i].ColumnName });
            }
            return filters;
        },
        applyFilterValues: function (initialUrl, columnName, values, skip) {
            var self = this;
            self.gridColumnFilters = null;
            var filters = self.jqContainer.find(".grid-filter");
            var url = URI(initialUrl).normalizeSearch().search();


            if (url.length > 0)
                url += "&";

            self.gridColumnFilters = "";
            if (!skip) {
                self.gridColumnFilters += this.getFilterQueryData(columnName, values);
            }

            if (this.options.multiplefilters) { //multiple filters enabled
                for (var i = 0; i < filters.length; i++) {
                    if ($(filters[i]).attr("data-name") != columnName) {
                        var filterData = this.parseFilterValues($(filters[i]).attr("data-filterdata"));
                        if (filterData.length == 0) continue;
                        if (self.gridColumnFilters.length > 0) self.gridColumnFilters += "&";
                        self.gridColumnFilters += this.getFilterQueryData($(filters[i]).attr("data-name"), filterData);
                    } else {
                        continue;
                    }
                }
            }

            if (self.gridColumnFilters.length > 0) {
                url += "&" + self.gridColumnFilters;
            }
            var fullSearch = url;
            if (fullSearch.indexOf("?") == -1) {
                fullSearch = "?" + fullSearch;
            }

            self.gridColumnFilters = fullSearch;

            self.currentPage = 1;

            if (self.gridFilterForm) {
                var formButton = $("#" + self.gridFilterForm.attr('id') + " input[type=submit],button[type=submit]")[0];
                var l = Ladda.create(formButton);
                l.start();
            }

            self.updateGrid(fullSearch, function () {
                if (l) {
                    l.stop();
                }
            });
        },
        ajaxify: function (options) {
            var self = this;
            self.currentPage = 1;
            self.loadPagedDataAction = options.getPagedData;
            self.loadDataAction = options.getData;
            self.gridFilterForm = options.gridFilterForm;
            self.gridSort = self.jqContainer.find("div.sorted a").attr('href');
            self.pageSetNum = 1;
            self.partitionSize = parseInt(self.jqContainer.find(".grid-pageSetLink").attr("data-partitionSize"));
            self.lastPageNum = parseInt(self.jqContainer.find(".grid-page-link:last").attr('data-page'));
            var $namedGrid = $('[data-gridname="' + self.jqContainer.data("gridname") + '"]');
            self.jqContainer = $namedGrid.length === 1 ? $namedGrid : self.jqContainer;
            self.preFilterFormAction = options.preFilterFormAction;
            self.preFilterFormClient = options.preFilterFormClient;

            if (self.gridSort) {
                if (self.gridSort.indexOf("grid-dir=0") != -1) {
                    self.gridSort = self.gridSort.replace("grid-dir=0", "grid-dir=1");
                } else {
                    self.gridSort = self.gridSort.replace("grid-dir=1", "grid-dir=0");
                }

                self.orginalSort = self.gridSort;
            }

            self.getGridUrl = function (griLoaddAction, search, renderRowsOnly) {
                var gridQuery = "?";

                if (self.gridFilterForm) {
                    gridQuery += self.gridFilterForm.serialize();
                } else if (search) {
                    gridQuery = search;
                }

                gridQuery = URI(gridQuery);

                var myColFilters = URI.parseQuery(search);
                if (myColFilters['grid-filter']) {
                    gridQuery.addSearch("grid-filter", myColFilters["grid-filter"]);
                }

                if (self.gridSort) {
                    var mySort = URI.parseQuery(self.gridSort);
                    gridQuery.addSearch("grid-column", mySort["grid-column"]);
                    gridQuery.addSearch("grid-dir", mySort["grid-dir"]);
                }

                var gridUrl = URI(griLoaddAction).addQuery(gridQuery.search().replace("?", ""));

                if (renderRowsOnly) {
                    gridUrl.addQuery(renderRowsOnly);
                } else {
                    gridUrl = gridUrl.removeSearch("renderRowsOnly").removeSearch("page");
                }
                gridUrl = URI.decode(gridUrl);
                return gridUrl;
            }

            self.updateGrid = function (search, callback, renderRowsOnly) {
                var gridUrl = self.getGridUrl(self.loadDataAction, search, renderRowsOnly);

                if (self.gridFilterForm) {
                    var formButton = $("#" + self.gridFilterForm.attr('id') + " input[type=submit],button[type=submit]")[0];
                    var l = Ladda.create(formButton);
                    l.start();
                }

                $.ajax({
                    url: gridUrl,
                    type: 'get',
                    data: {},
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    async: true,
                    cache: false,
                    timeout: 20000
                }).done(function (response) {
                    self.jqContainer.html($('<div>' + response.Html + '</div>').find("div.grid-wrap"));
                    self.initFilters();
                    self.pageSetNum = 1;
                    self.notifyOnGridLoaded(response, $.Event("GridLoaded"));
                }).fail(function (response) {
                    self.notifyOnGridError(response, $.Event("GridError"));
                }).always(function (response) {
                    if (callback) {
                        callback(response);
                    }

                    if (l) {
                        l.stop();
                    }
                });
            };

            self.SetupGridHeaderEvents = function () {
                self.jqContainer.on('click', '.grid-header-title > a', function (e) {
                    self.gridSort = '';
                    e.preventDefault();
                    self.currentPage = 1;

                    if (self.gridFilterForm) {
                        var formButton = $("#" + self.gridFilterForm.attr('id') + " input[type=submit],button[type=submit]")[0];
                        var l = Ladda.create(formButton);
                        l.start();
                    }

                    // remove grid sort arrows
                    self.jqContainer.find(".grid-header-title").removeClass("sorted-asc");
                    self.jqContainer.find(".grid-header-title").removeClass("sorted-desc");

                    var mySearch = $(this).attr('href');
                    var isAscending = mySearch.indexOf("grid-dir=1") !== -1;
                    self.gridSort = mySearch.substr(mySearch.match(/grid-column=\w+/).index);

                    self.updateGrid(mySearch, function () {
                        if (l) {
                            l.stop();
                        }
                    });

                    // update link to sort in opposite direction
                    if (isAscending) {
                        $(this).attr('href', mySearch.replace("grid-dir=1", "grid-dir=0"));
                    } else {
                        $(this).attr('href', mySearch.replace("grid-dir=0", "grid-dir=1"));
                    }

                    // add new grid sort arrow
                    var newSortClass = isAscending ? "sorted-desc" : "sorted-asc";
                    $(this).parent(".grid-header-title").addClass(newSortClass);
                    $(this).parent(".grid-header-title").children("span").remove();
                    $(this).parent(".grid-header-title").append($("<span/>").addClass("grid-sort-arrow"));
                });
            };

            var filterSuccess = function () {
                self.currentPage = 1;
                var formButton = $("#" + self.gridFilterForm.attr('id') + " input[type=submit],button[type=submit]")[0];
                var l = Ladda.create(formButton);
                l.start();
                self.updateGrid(location.search, function () {
                    l.stop();
                });
            };

            if (self.gridFilterForm) {
                self.gridFilterForm.on('submit', function (e) {
                    e.preventDefault();
                    if (self.preFilterFormAction) {
                        $.post(self.preFilterFormAction, self.gridFilterForm.serialize()).done(function (preFilterResult) {
                            filterSuccess();
                        }).fail(function (response) {
                            self.notifyOnPreFilterError(response, $.Event("PreFilterError"));
                        });
                    } else if (self.preFilterFormClient) {
                        self.preFilterFormClient().done(function (preResult) {
                            filterSuccess();
                        });
                    }
                    else {
                        filterSuccess();
                    }
                });
            }

            self.loadNextPageSet = function () {
                // load next page set
                var pageSetNum = self.pageSetNum + 1;
                self.partitionSize = parseInt(self.jqContainer.find(".grid-pageSetLink").attr("data-partitionSize"));
                self.lastPageNum = parseInt(self.jqContainer.find(".grid-page-link:last").attr('data-page'));
                var nextPageNum = (pageSetNum - 1) * self.partitionSize + 2;

                if (nextPageNum <= self.lastPageNum) {
                    self.jqContainer.find(".grid-page-link").each(function (index, item) {
                        var currentPage = parseInt($(item).attr('data-page'));
                        if (currentPage > 1 && currentPage < self.lastPageNum) {
                            // loading next set of pages
                            if (nextPageNum < self.lastPageNum) {
                                $(item).show();
                                $(item).attr('data-page', nextPageNum).text(nextPageNum);
                            } else {
                                $(item).hide();
                            }

                            nextPageNum++;
                        }
                    });

                    if (pageSetNum == 2) {
                        self.jqContainer.find(".grid-pageSetLink.prev").show();
                    } else if (pageSetNum * self.partitionSize + 1 >= self.lastPageNum) {
                        self.jqContainer.find(".grid-pageSetLink.next").hide();
                    }

                    $(this).attr('data-pageset', pageSetNum + 1);
                    self.jqContainer.find(".grid-pageSetLink.prev").attr('data-pageset', pageSetNum - 1);
                    self.pageSetNum = pageSetNum;

                    if (self.pageSetNum * self.partitionSize >= self.lastPageNum) {
                        // hide next page set link
                        self.jqContainer.find(".grid-pageSetLink.next").hide();
                    }
                }
            };

            self.loadPreviousPageSet = function () {
                // load previous page set
                self.partitionSize = parseInt(self.jqContainer.find(".grid-pageSetLink").attr("data-partitionSize"));
                self.lastPageNum = parseInt(self.jqContainer.find(".grid-page-link:last").attr('data-page'));
                var pageSetNum = self.pageSetNum - 1;
                var incrementSize = self.partitionSize - 2;

                if ((pageSetNum * self.partitionSize) <= self.lastPageNum) {
                    var newPage = pageSetNum * self.partitionSize - incrementSize;

                    self.jqContainer.find(".grid-page-link").each(function (index, item) {
                        var currentPage = parseInt($(item).attr('data-page'));
                        if (currentPage > 1) {
                            if (currentPage < self.lastPageNum) {
                                // loading previous set of pages
                                $(item).show();
                                $(item).attr('data-page', newPage).text(newPage);
                                newPage++;
                            }
                        }
                    });

                    if (pageSetNum == 1) {
                        self.jqContainer.find(".grid-pageSetLink.prev").hide();
                    }

                    if (pageSetNum > 1) {
                        $(this).attr('data-pageset', pageSetNum - 1);
                    }

                    self.jqContainer.find(".grid-pageSetLink.next").attr('data-pageset', pageSetNum + 1);

                    if (pageSetNum * self.partitionSize < self.lastPageNum) {
                        self.jqContainer.find(".grid-pageSetLink.next").show();
                    }
                    self.pageSetNum = pageSetNum;
                }
            };

            self.setupPagerLinkEvents = function () {
                self.jqContainer.on("click", ".grid-next-page", function (e) {
                    e.preventDefault();
                    self.currentPage++;
                    self.loadPage();
                    if (self.currentPage >= self.partitionSize * self.pageSetNum + 2) {
                        // load next page set
                        self.loadNextPageSet();
                    }
                    self.jqContainer.find(".pagination li.active").removeClass("active").children("a").attr('href', '#');
                    self.jqContainer.find("a[data-page=" + self.currentPage + "]").parent("li").addClass("active");
                });

                self.jqContainer.on("click", ".grid-prev-page", function (e) {
                    e.preventDefault();
                    self.currentPage--;
                    self.loadPage();

                    if (self.currentPage > 1 &&
                        self.currentPage < self.partitionSize * (self.pageSetNum - 1) + 2) {
                        self.loadPreviousPageSet();
                    }

                    self.jqContainer.find(".pagination li.active").removeClass("active").children("a").attr('href', '#');
                    self.jqContainer.find("a[data-page=" + self.currentPage + "]").parent("li").addClass("active");
                });

                self.jqContainer.on("click", ".grid-page-link", function (e) {
                    e.preventDefault();
                    var pageNumber = $(this).attr('data-page');
                    var oldPageNumber = self.currentPage;
                    self.currentPage = pageNumber;
                    self.loadPage();
                    self.jqContainer.find(".pagination li.active").removeClass("active").children("a").attr('href', '#');
                    $(this).parent("li").addClass("active");

                    if (self.currentPage == 1 && oldPageNumber != 1) {
                        // load first page set
                        self.pageSetNum = 2;
                        self.loadPreviousPageSet();
                    } else if (self.currentPage == self.lastPageNum && self.currentPage != oldPageNumber) {
                        // load last page set
                        self.pageSetNum = Math.ceil(self.lastPageNum / self.partitionSize) - 1;
                        self.loadNextPageSet();
                        self.jqContainer.find(".grid-pageSetLink.prev").show();
                    }
                });

                self.jqContainer.on("click", ".grid-pageSetLink.next", function (e) {
                    e.preventDefault();
                    self.loadNextPageSet();

                    // reload new selected page
                    self.jqContainer.find("li.active .grid-page-link").click();
                });

                self.jqContainer.on("click", ".grid-pageSetLink.prev", function (e) {
                    e.preventDefault();
                    self.loadPreviousPageSet();

                    // reload new selected page
                    self.jqContainer.find("li.active .grid-page-link").click();
                });
            };

            self.loadPage = function () {
                var dfd = new $.Deferred();

                var gridTableBody = self.jqContainer.find(".grid-footer").closest(".grid-wrap").find("tbody");
                var nextPageLink = self.jqContainer.find(".grid-next-page");
                var prevPageLink = self.jqContainer.find(".grid-prev-page");
                self.partitionSize = parseInt(self.jqContainer.find(".grid-pageSetLink").attr("data-partitionSize"));
                self.lastPageNum = parseInt(self.jqContainer.find(".grid-page-link:last").attr('data-page'));

                if (self.gridFilterForm) {
                    var formButton = $("#" + self.gridFilterForm.attr('id') + " input[type=submit],button[type=submit]")[0];
                    var l = Ladda.create(formButton);
                    l.start();
                }

                var gridQuery = self.getGridUrl(self.loadPagedDataAction, self.gridColumnFilters, null);
                var gridUrl = URI(gridQuery).addQuery("page", self.currentPage);
                gridUrl = URI.decode(gridUrl);

                $.get(gridUrl)
                    .done(function (response) {
                        gridTableBody.html("");
                        gridTableBody.append(response.Html);
                        if (self.currentPage == self.lastPageNum) {
                            nextPageLink.hide();
                        } else {
                            nextPageLink.show();
                        }

                        if (self.currentPage == 1) {
                            prevPageLink.hide();
                        } else {
                            prevPageLink.show();
                        }

                        if (l) {
                            l.stop();
                        }

                        self.notifyOnGridLoaded(response, $.Event("GridLoaded"));
                    })
                    .fail(function () {
                        self.notifyOnGridError(null, $.Event("GridError"));
                    }).always(function (response) {
                        dfd.resolve(response);
                    });

                return dfd.promise();
            };

            this.pad = function (query) {
                if (query.length == 0) return "?page=";
                return query + "&page=";
            };

            self.SetupGridHeaderEvents();
            self.setupPagerLinkEvents();
        },
        onGridLoaded: function (func) {
            this.events.push({ name: "onGridLoaded", callback: func });
        },
        notifyOnGridLoaded: function (data, e) {
            e.data = data;
            this.notifyEvent("onGridLoaded", e);
        },
        refreshFullGrid: function () {
            var self = this;
            var dfd = new $.Deferred();
            self.currentPage = 1;
            self.updateGrid(location.search, function (result) {
                dfd.resolve(result);
            });

            return dfd.promise();
        },
        refreshPartialGrid: function () {
            var self = this;
            var dfd = new $.Deferred();
            var gridQuery = location.search;

            //if (self.gridSort) {
            //    gridQuery += self.gridSort.replace("?", "");
            //}

            if (self.gridColumnFilters) {
                gridQuery += (gridQuery.length == 0 ? "&" : "") + self.gridColumnFilters.replace("?", "");
            }

            var gridSettings = "?" + gridQuery;
            var gridPageSettings = "";
            if (self.currentPage > 1) {
                gridPageSettings = "renderRowsOnly=false&page=" + self.currentPage;
            }
            self.updateGrid(gridSettings, function (result) {
                var pageNumber = self.currentPage;
                self.jqContainer.find(".pagination li.active").removeClass("active").children("a").attr('href', '#');
                self.jqContainer.find('[data-page~=' + pageNumber + ']').parent("li").addClass("active");

                if (pageNumber == 1) {
                    // load first page set
                    self.pageSetNum = 2;
                    self.loadPreviousPageSet();
                } else if (pageNumber == self.lastPageNum) {
                    // load last page set
                    self.pageSetNum = Math.ceil(self.lastPageNum / self.partitionSize) - 1;
                    self.loadNextPageSet();
                    self.jqContainer.find(".grid-prev-page").show();
                    self.jqContainer.find(".grid-next-page").hide();
                    self.jqContainer.find(".grid-pageSetLink.prev").show();
                } else {
                    self.jqContainer.find(".grid-prev-page").show();
                }

                dfd.resolve(result);
            }, gridPageSettings);

            return dfd.promise();
        },
        clearGridFilters: function () {
            var self = this;
            self.gridColumnFilters = null;
            self.gridSort = self.orginalSort;
        },
        clearGrid: function () {
            var self = this;

            self.gridColumnFilters = null;
            self.gridSort = self.orginalSort;
            self.currentPage = 1;

            self.jqContainer.html($('<div></div>').find("div.grid-wrap"));
            self.initFilters();
            self.pageSetNum = 1;
            self.notifyOnGridLoaded(null, $.Event("GridLoaded"));
        },
        onGridError: function (func) {
            this.events.push({ name: "onGridError", callback: func });
        },
        notifyOnGridError: function (data, e) {
            e.data = data;
            this.notifyEvent("onGridError", e);
        },
        onPreFilterError: function (func) {
            this.events.push({ name: "onPreFilterError", callback: func });
        },
        notifyOnPreFilterError: function (data, e) {
            e.data = data;
            this.notifyEvent("onPreFilterError", e);
        }
    });
})(jQuery);