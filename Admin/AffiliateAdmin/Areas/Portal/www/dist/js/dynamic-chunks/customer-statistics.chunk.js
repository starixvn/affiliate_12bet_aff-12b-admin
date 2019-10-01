(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["customer-statistics"],{

/***/ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/actions/index.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/customer/customer-statistics/actions/index.js ***!
  \****************************************************************************************/
/*! exports provided: setMonthDate, fetchData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setMonthDate", function() { return setMonthDate; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "fetchData", function() { return fetchData; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _types__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./types */ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/actions/types.js");
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");




var setMonthDate = function setMonthDate(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_2__["SET_MONTHDATE"],
    data: data
  };
};
var fetchData = function fetchData(param) {
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee() {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              url = 'Portal/CustomerStatistics/GetCustomerStatistics';
              _context.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__["post"])(url, param);

            case 3:
              response = _context.sent;
              return _context.abrupt("return", response && response.status === 200 ? response.data : null);

            case 5:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    }))
  );
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/components/AppContainer.jsx":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/customer/customer-statistics/components/AppContainer.jsx ***!
  \***************************************************************************************************/
/*! exports provided: AppContainer, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppContainer", function() { return AppContainer; });
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/components/Content.jsx");



var AppContainer = function AppContainer(props) {
  return react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_2__["default"], null));
};
/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/components/Content.jsx":
/*!**********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/customer/customer-statistics/components/Content.jsx ***!
  \**********************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @9softstudio/react-monthpicker */ "./node_modules/@9softstudio/react-monthpicker/dist/index.js");
/* harmony import */ var _9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_14___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_14__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/modules/constants.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");
/* harmony import */ var _user_panel_actions__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ../../user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/actions/index.js");





















var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Content).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "fetchData",
    /*#__PURE__*/
    function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.mark(function _callee(monthDate) {
        var affId, param, data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                _this.setState({
                  isLoading: true
                });

                _this.props.setMonthDate(monthDate);

                affId = _this.props.affId;
                param = {
                  monthDate: monthDate,
                  affId: affId
                };
                _context.next = 6;
                return _this.props.fetchData(param);

              case 6:
                data = _context.sent;

                _this.setState({
                  data: data,
                  isLoading: false
                });

              case 8:
              case "end":
                return _context.stop();
            }
          }
        }, _callee);
      }));

      return function (_x) {
        return _ref.apply(this, arguments);
      };
    }());

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleSelectMonth", function (month, year) {
      var _context2;

      var monthDate = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context2 = "".concat(month, "/1/")).call(_context2, year);

      _this.fetchData(monthDate);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleFormatMonthPicker", function (month, year) {
      var _context3;

      return _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context3 = "".concat(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["shortedMonthNames"][month - 1], "-")).call(_context3, year);
    });

    _this.state = {
      data: null,
      isLoading: false
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      this.fetchData(this.props.today);
    }
  }, {
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var _this$props = this.props,
          affId = _this$props.affId,
          today = _this$props.today;

      if (prevProps.affId !== affId) {
        this.fetchData(today);
      }
    }
  }, {
    key: "_renderHeader",
    value: function _renderHeader(monthDate) {
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("thead", null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("tr", null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("th", null, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].Statistics), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("th", {
        className: "width-30 month-picker"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "position-relative d-inline-block"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_14___default.a, {
        minMonth: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["minMonthDate"].month,
        minYear: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["minMonthDate"].year,
        selectedDropdownYear: monthDate.getFullYear(),
        selectedMonth: monthDate.getMonth() + 1,
        onSelect: this.handleSelectMonth,
        onFormat: this.handleFormatMonthPicker,
        iconElement: react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        })
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Spinner"], {
        className: this.state.isLoading ? "" : "d-none",
        size: "sm",
        color: "secondary"
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("th", {
        className: "width-30"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].UpToNow)));
    }
  }, {
    key: "_renderData",
    value: function _renderData(monthDate, data) {
      var _context4,
          _context5,
          _context6,
          _context7,
          _context8,
          _this2 = this;

      data = data || {};
      var DepositByMonth = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["formatDecimalWithCurrencySymbol"])(data.DepositByMonth);
      var DepositTotal = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["formatDecimalWithCurrencySymbol"])(data.DepositTotal);
      var TurnoverByMonth = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["formatDecimalWithCurrencySymbol"])(data.TurnoverByMonth);
      var TurnoverTotal = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["formatDecimalWithCurrencySymbol"])(data.TurnoverTotal);
      var WinlostByMonth = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["formatDecimalWithCurrencySymbol"])(data.WinlostByMonth);
      var WinlostTotal = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["formatDecimalWithCurrencySymbol"])(data.WinlostTotal);

      var startMonthString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context4 = "".concat(monthDate.getMonth() + 1, "/1/")).call(_context4, monthDate.getFullYear());

      var endMonth = new Date(monthDate.getFullYear(), monthDate.getMonth() + 1, 0);

      var endMonthString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context5 = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context6 = "".concat(endMonth.getMonth() + 1, "/")).call(_context6, endMonth.getDate(), "/")).call(_context5, endMonth.getFullYear());

      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("tbody", null, this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].NewSignups, _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context7 = "".concat(Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.NewSignUpByMonth), "/")).call(_context7, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.NewFdmByMonth)), _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context8 = "".concat(Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.NewSignUpTotal), "/")).call(_context8, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.NewFdmTotal))), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].TotalActive, data.ActivePlayerByMonth > 0 ? react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToPlayerDetail({
            productId: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["selectedAllProduct"],
            monthDate: startMonthString,
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.ActivePlayerByMonth)) : Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.ActivePlayerByMonth), _modules_constants__WEBPACK_IMPORTED_MODULE_16__["resource"].NA), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].TotalQualified, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.QualifiedMemberByMonth), _modules_constants__WEBPACK_IMPORTED_MODULE_16__["resource"].NA), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].TotalDeposits, DepositByMonth, DepositTotal), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].Turnover, TurnoverByMonth, TurnoverTotal), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].Winlost, WinlostByMonth, WinlostTotal), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].TrackingRate, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToTrackingStatistics({
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.UniqueClickByMonth), "/", Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.TotalClickByMonth)), _modules_constants__WEBPACK_IMPORTED_MODULE_16__["resource"].NA), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].TrackingRatePC, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToReferrers({
            fromDate: startMonthString,
            toDate: endMonthString,
            deviceType: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["deviceTypes"][_modules_constants__WEBPACK_IMPORTED_MODULE_17__["pcLaptop"]],
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.UniqueClickPcLaptopByMonth), "/", Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.TotalClickPcLaptopByMonth)), _modules_constants__WEBPACK_IMPORTED_MODULE_16__["resource"].NA), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["rowTitle"].TrackingRateMobile, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToReferrers({
            fromDate: startMonthString,
            toDate: endMonthString,
            deviceType: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["deviceTypes"][_modules_constants__WEBPACK_IMPORTED_MODULE_17__["mobile"]],
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.UniqueClickMobileByMonth), "/", Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_18__["format"])(data.TotalClickMobileByMonth)), _modules_constants__WEBPACK_IMPORTED_MODULE_16__["resource"].NA));
    }
  }, {
    key: "_buildRow",
    value: function _buildRow(title, dataByMonth, dataUpToNow) {
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("tr", {
        key: title
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("td", {
        className: "font-weight-bold"
      }, title), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("td", {
        className: "text-right"
      }, dataByMonth), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("td", {
        className: "text-right"
      }, dataUpToNow));
    }
  }, {
    key: "render",
    value: function render() {
      var data = this.state.data;
      var monthDate = new Date(this.props.monthDate);
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "my-2 table-responsive"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Table"], {
        id: "statistic-data-table"
      }, this._renderHeader(monthDate), this._renderData(monthDate, data)));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref2) {
  var affCommonInfo = _ref2.affCommonInfo,
      today = _ref2.today,
      customerStatistics = _ref2.customerStatistics;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    monthDate: customerStatistics.monthDate,
    today: today,
    affId: affId
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setMonthDate: _actions__WEBPACK_IMPORTED_MODULE_20__["setMonthDate"],
    redirectToPlayerDetail: _user_panel_actions__WEBPACK_IMPORTED_MODULE_19__["redirectToPlayerDetail"],
    redirectToTrackingStatistics: _user_panel_actions__WEBPACK_IMPORTED_MODULE_19__["redirectToTrackingStatistics"],
    redirectToReferrers: _user_panel_actions__WEBPACK_IMPORTED_MODULE_19__["redirectToReferrers"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_20__["fetchData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/modules/constants.js":
/*!********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/customer/customer-statistics/modules/constants.js ***!
  \********************************************************************************************/
/*! exports provided: rowTitle, resource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "rowTitle", function() { return rowTitle; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
var rowTitle = {
  Statistics: "Statistics",
  UpToNow: "Up To Now",
  NewSignups: "New Signups/FDM",
  TotalActive: "Total Active Players",
  TotalQualified: "Total Qualified Active Players",
  TotalDeposits: "Total Deposits",
  Turnover: "Turnover",
  Winlost: "Winlost",
  TrackingRate: "Tracking Rate (Unique/Total)",
  TrackingRatePC: "Tracking Rate for PC/Laptop (Unique/Total) New!!!!!",
  TrackingRateMobile: "Tracking Rate for Mobile (Unique/Total) New!!!!!"
};
var resource = {
  NA: "N/A"
};

/***/ })

}]);
//# sourceMappingURL=customer-statistics.chunk.js.map