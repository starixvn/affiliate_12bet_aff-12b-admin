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
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee() {
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
  }));
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
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_2__["default"], null));
};
/* harmony default export */ __webpack_exports__["default"] = (/*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

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
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @9softstudio/react-monthpicker */ "./node_modules/@9softstudio/react-monthpicker/dist/index.js");
/* harmony import */ var _9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_15___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_15__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/modules/constants.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ../../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");
/* harmony import */ var _user_panel_actions__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ../../user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/customer/customer-statistics/actions/index.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ../../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");













function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }












var Content = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Content, _PureComponent);

  var _super = _createSuper(Content);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default()(this, Content);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "fetchData", /*#__PURE__*/function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_4___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3___default.a.mark(function _callee(monthDate) {
        var affId, param, data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3___default.a.wrap(function _callee$(_context) {
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

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleSelectMonth", function (month, year) {
      var _context2;

      var monthDate = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context2 = "".concat(month, "/1/")).call(_context2, year);

      _this.fetchData(monthDate);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleFormatMonthPicker", function (month, year) {
      var _context3;

      return _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context3 = "".concat(_modules_constants__WEBPACK_IMPORTED_MODULE_18__["shortedMonthNames"][month - 1], "-")).call(_context3, year);
    });

    _this.state = {
      data: null,
      isLoading: false
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default()(Content, [{
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
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("thead", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("tr", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("th", null, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].Statistics), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("th", {
        className: "width-30 month-picker"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("div", {
        className: "position-relative d-inline-block"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(_9softstudio_react_monthpicker__WEBPACK_IMPORTED_MODULE_15___default.a, {
        minMonth: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["minMonthDate"].month,
        minYear: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["minMonthDate"].year,
        selectedDropdownYear: monthDate.getFullYear(),
        selectedMonth: monthDate.getMonth() + 1,
        onSelect: this.handleSelectMonth,
        onFormat: this.handleFormatMonthPicker,
        iconElement: /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        })
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Spinner"], {
        className: this.state.isLoading ? "" : "d-none",
        size: "sm",
        color: "secondary"
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("th", {
        className: "width-30"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].UpToNow)));
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
      var _this$props2 = this.props,
          isDisplayTurnOver = _this$props2.isDisplayTurnOver,
          isDisplayActivePlayer = _this$props2.isDisplayActivePlayer;
      var DepositByMonth = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["formatDecimalWithCurrencySymbol"])(data.DepositByMonth);
      var DepositTotal = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["formatDecimalWithCurrencySymbol"])(data.DepositTotal);
      var TurnoverByMonth = isDisplayTurnOver && Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["formatDecimalWithCurrencySymbol"])(data.TurnoverByMonth);
      var TurnoverTotal = isDisplayTurnOver && Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["formatDecimalWithCurrencySymbol"])(data.TurnoverTotal);
      var WinlostByMonth = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["formatDecimalWithCurrencySymbol"])(data.WinlostByMonth);
      var WinlostTotal = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["formatDecimalWithCurrencySymbol"])(data.WinlostTotal);

      var startMonthString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context4 = "".concat(monthDate.getMonth() + 1, "/1/")).call(_context4, monthDate.getFullYear());

      var endMonth = new Date(monthDate.getFullYear(), monthDate.getMonth() + 1, 0);

      var endMonthString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context5 = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context6 = "".concat(endMonth.getMonth() + 1, "/")).call(_context6, endMonth.getDate(), "/")).call(_context5, endMonth.getFullYear());

      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("tbody", null, this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].NewSignups, _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context7 = "".concat(Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.NewSignUpByMonth), "/")).call(_context7, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.NewFdmByMonth)), _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context8 = "".concat(Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.NewSignUpTotal), "/")).call(_context8, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.NewFdmTotal))), isDisplayActivePlayer && this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].TotalActive, data.ActivePlayerByMonth > 0 ? /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToPlayerDetail({
            productId: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["selectedAllProduct"],
            monthDate: startMonthString,
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.ActivePlayerByMonth)) : Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.ActivePlayerByMonth), _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].NA), isDisplayActivePlayer && this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].TotalQualified, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.QualifiedMemberByMonth), _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].NA), isDisplayActivePlayer && this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].TotalDeposits, DepositByMonth, DepositTotal), isDisplayTurnOver && this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].Turnover, TurnoverByMonth, TurnoverTotal), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].Winlost, WinlostByMonth, WinlostTotal), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].TrackingRate, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToTrackingStatistics({
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.UniqueClickByMonth), "/", Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.TotalClickByMonth)), _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].NA), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].TrackingRatePC, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToReferrers({
            fromDate: startMonthString,
            toDate: endMonthString,
            deviceType: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["deviceTypes"][_modules_constants__WEBPACK_IMPORTED_MODULE_18__["pcLaptop"]],
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.UniqueClickPcLaptopByMonth), "/", Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.TotalClickPcLaptopByMonth)), _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].NA), this._buildRow(_modules_constants__WEBPACK_IMPORTED_MODULE_17__["rowTitle"].TrackingRateMobile, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("a", {
        href: "javascript:;",
        onClick: function onClick() {
          return _this2.props.redirectToReferrers({
            fromDate: startMonthString,
            toDate: endMonthString,
            deviceType: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["deviceTypes"][_modules_constants__WEBPACK_IMPORTED_MODULE_18__["mobile"]],
            isClickLink: true
          });
        }
      }, Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.UniqueClickMobileByMonth), "/", Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_19__["format"])(data.TotalClickMobileByMonth)), _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].NA));
    }
  }, {
    key: "_buildRow",
    value: function _buildRow(title, dataByMonth, dataUpToNow) {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("tr", {
        key: title
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("td", {
        className: "font-weight-bold"
      }, title), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("td", {
        className: "text-right"
      }, dataByMonth), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("td", {
        className: "text-right"
      }, dataUpToNow));
    }
  }, {
    key: "render",
    value: function render() {
      var data = this.state.data;
      var monthDate = new Date(this.props.monthDate);
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("div", {
        className: "my-2 table-responsive"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Table"], {
        id: "statistic-data-table"
      }, this._renderHeader(monthDate), this._renderData(monthDate, data)));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_12__["PureComponent"]);

function mapStateToProps(_ref2) {
  var affCommonInfo = _ref2.affCommonInfo,
      today = _ref2.today,
      customerStatistics = _ref2.customerStatistics,
      claimsPrincipal = _ref2.claimsPrincipal;
  var affId = affCommonInfo.affId;
  var isDisplayTurnOver = claimsPrincipal.hasReadClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_22__["claimResource"].TurnOver);
  var isDisplayActivePlayer = claimsPrincipal.hasReadClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_22__["claimResource"].AffPlayerDetail);
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    monthDate: customerStatistics.monthDate,
    today: today,
    affId: affId,
    isDisplayTurnOver: isDisplayTurnOver,
    isDisplayActivePlayer: isDisplayActivePlayer
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_14__["bindActionCreators"])({
    setMonthDate: _actions__WEBPACK_IMPORTED_MODULE_21__["setMonthDate"],
    redirectToPlayerDetail: _user_panel_actions__WEBPACK_IMPORTED_MODULE_20__["redirectToPlayerDetail"],
    redirectToTrackingStatistics: _user_panel_actions__WEBPACK_IMPORTED_MODULE_20__["redirectToTrackingStatistics"],
    redirectToReferrers: _user_panel_actions__WEBPACK_IMPORTED_MODULE_20__["redirectToReferrers"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_21__["fetchData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_13__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

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

/***/ }),

/***/ "./node_modules/@9softstudio/react-monthpicker/dist/index.js":
/*!*******************************************************************!*\
  !*** ./node_modules/@9softstudio/react-monthpicker/dist/index.js ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

!function(e,t){ true?module.exports=t(__webpack_require__(/*! react */ "./node_modules/react/index.js")):undefined}(window,function(e){return function(e){var t={};function n(r){if(t[r])return t[r].exports;var o=t[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}return n.m=e,n.c=t,n.d=function(e,t,r){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:r})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)n.d(r,o,function(t){return e[t]}.bind(null,o));return r},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="/dist/",n(n.s=5)}([function(t,n){t.exports=e},function(e,t,n){e.exports=n(7)()},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});t.defaultMinYear=2e3,t.getCurrentDate=function(){return new Date}},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=l(n(0)),a=l(n(1)),u=n(2);function l(e){return e&&e.__esModule?e:{default:e}}var c=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.handleSelect=function(e){var t=e.target.value;n.props.onSelect(Number(t))},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.default.Component),r(t,[{key:"_getYearList",value:function(){var e=this.props,t=e.minYear,n=e.maxYear;if(t>(n=n||(0,u.getCurrentDate)().getFullYear())){var r=t;t=n,n=r}for(var o=[],a=n;a>=t;a--)o.push(a);return o}},{key:"render",value:function(){var e=this.props.selectedDropdownYear,t=this._getYearList(),n=e||t.length&&t[0];return o.default.createElement("div",{className:"mp-header"},o.default.createElement("select",{className:"mp-select-year",value:n,onChange:this.handleSelect},t.map(function(e){return o.default.createElement("option",{key:e,value:e},e)})))}}]),t}();c.propTypes={minYear:a.default.number,maxYear:a.default.number,selectedDropdownYear:a.default.number,onSelect:a.default.func},c.defaultProps={minYear:u.defaultMinYear,onSelect:function(){}},t.default=c},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=l(n(0)),a=l(n(1)),u=n(2);function l(e){return e&&e.__esModule?e:{default:e}}var c=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n._renderMonth=function(){for(var e=n.props.monthNames,t=[],r=0;r<e.length;r++){var o=e[r];t.push(n._buildRow(o,r))}return t},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.default.Component),r(t,[{key:"_buildRow",value:function(e,t){var n=this,r=this.props,a=r.selectedDropdownYear,l=r.selectedYear,c=void 0===this.props.selectedMonth?(0,u.getCurrentDate)().getMonth():this.props.selectedMonth;return o.default.createElement("tr",{key:t,className:"mp-row"},e.map(function(r,u){var i=t*e.length+u,s=n.props.hasRange&&!n._isInRange(i),f=s?"mp-disabled":"",d=a===l&&c===i?"mp-active":"";return o.default.createElement("td",{key:r},o.default.createElement("button",{type:"button",className:"mp-btn-month "+f+" "+d,onClick:function(){return!s&&n.props.onSelect(i)}},r))}))}},{key:"_isInRange",value:function(e){var t=this.props,n=t.minMonth,r=t.minYear,o=t.maxMonth,a=t.maxYear,l=t.selectedDropdownYear,c=(0,u.getCurrentDate)();return a=a||c.getFullYear(),o=o?o-1:c.getMonth(),((l=l||c.getFullYear())>r||l===r&&e>=n-1)&&(l<a||l===a&&e<=o)}},{key:"render",value:function(){return o.default.createElement("table",{className:"mp-month-table"},o.default.createElement("tbody",null,this._renderMonth()))}}]),t}();c.propTypes={monthNames:a.default.arrayOf(a.default.arrayOf(a.default.string)),minMonth:a.default.number,minYear:a.default.number,maxMonth:a.default.number,maxYear:a.default.number,selectedDropdownYear:a.default.number,selectedYear:a.default.number,selectedMonth:a.default.number,hasRange:a.default.bool},c.defaultProps={hasRange:!0,monthNames:[["Jan","Feb","Mar"],["Apr","May","Jun"],["Jul","Aug","Sep"],["Oct","Nov","Dec"]],minMonth:1,minYear:u.defaultMinYear,onSelect:function(){}},t.default=c},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.YearSelector=t.MonthList=void 0;var r=u(n(6)),o=u(n(4)),a=u(n(3));function u(e){return e&&e.__esModule?e:{default:e}}t.MonthList=o.default,t.YearSelector=a.default,t.default=r.default},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=i(n(0)),a=i(n(1)),u=n(2),l=i(n(3)),c=i(n(4));function i(e){return e&&e.__esModule?e:{default:e}}var s=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));f.call(n);var r=n.props,a=r.open,l=r.selectedDropdownYear,c=r.selectedMonth,i=r.selectedYear,s=(0,u.getCurrentDate)(),d=l||s.getFullYear();return n.state={open:a,selectedDropdownYear:d,selectedYear:i||d,selectedMonth:c?c-1:s.getMonth()},n.container=o.default.createRef(),n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.default.Component),r(t,[{key:"componentDidMount",value:function(){document.addEventListener("click",this.handleClickOutSide,!0)}},{key:"componentWillUnmount",value:function(){document.removeEventListener("click",this.handleClickOutSide,!0)}},{key:"componentDidUpdate",value:function(e,t){var n=this.props,r=n.selectedDropdownYear,o=n.selectedMonth,a=n.selectedYear;o===e.selectedMonth&&a===e.selectedYear&&r===e.selectedDropdownYear||this.setState({selectedYear:r,selectedMonth:o-1,selectedDropdownYear:r})}},{key:"render",value:function(){var e=this.state,t=e.open,n=e.selectedDropdownYear,r=e.selectedYear,a=e.selectedMonth,u=this.props,i=u.monthNames,s=u.hasRange,f=u.minMonth,d=u.minYear,p=u.maxMonth,m=u.maxYear,h=u.isReadonly,b=u.onFormat,y=u.iconElement,v=u.enable,_=b(a+1,r),g=v?"mp-form-container":"mp-form-container disable";return o.default.createElement("div",{className:"rmp-main-container"},o.default.createElement("div",{className:g,onClick:this.toggle},o.default.createElement("input",{type:"text",className:"mp-form-input",value:_,readOnly:h}),y),o.default.createElement("div",{className:"mp-container",style:{display:t?"block":"none"},ref:this.container},o.default.createElement(l.default,{minYear:d,maxYear:m,selectedDropdownYear:n,onSelect:this.handleChangeYear}),o.default.createElement(c.default,{monthNames:i,hasRange:s,selectedDropdownYear:n,selectedMonth:a,selectedYear:r,minMonth:f,minYear:d,maxMonth:p,maxYear:m,onSelect:this.handleSelectMonth})))}}]),t}();s.propTypes={open:a.default.bool,minMonth:a.default.number,minYear:a.default.number,maxMonth:a.default.number,maxYear:a.default.number,monthNames:a.default.arrayOf(a.default.arrayOf(a.default.string)),hasRange:a.default.bool,selectedDropdownYear:a.default.number,selectedMonth:a.default.number,onSelect:a.default.func,onFormat:a.default.func,isReadonly:a.default.bool,iconElement:a.default.any,enable:a.default.bool},s.defaultProps={open:!1,onSelect:function(){},onFormat:function(e,t){return(e<10?"0"+e:e)+"/"+t},isReadonly:!0,enable:!0};var f=function(){var e=this;this.toggle=function(){e.props.enable&&e.setState(function(e){return{open:!e.open}})},this.handleChangeYear=function(t){e.setState({selectedDropdownYear:t})},this.handleSelectMonth=function(t){var n=e.state.selectedDropdownYear;e.setState({selectedMonth:t,selectedYear:n,open:!1},function(){return e.props.onSelect(t+1,n)})},this.handleClickOutSide=function(t){var n=e.container.current;n&&!n.contains(t.target)&&e.state.open&&e.setState({open:!1})}};t.default=s},function(e,t,n){"use strict";var r=n(8);function o(){}e.exports=function(){function e(e,t,n,o,a,u){if(u!==r){var l=new Error("Calling PropTypes validators directly is not supported by the `prop-types` package. Use PropTypes.checkPropTypes() to call them. Read more at http://fb.me/use-check-prop-types");throw l.name="Invariant Violation",l}}function t(){return e}e.isRequired=e;var n={array:e,bool:e,func:e,number:e,object:e,string:e,symbol:e,any:e,arrayOf:t,element:e,instanceOf:t,node:e,objectOf:t,oneOf:t,oneOfType:t,shape:t,exact:t};return n.checkPropTypes=o,n.PropTypes=n,n}},function(e,t,n){"use strict";e.exports="SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED"}])});

/***/ })

}]);
//# sourceMappingURL=customer-statistics.chunk.js.map