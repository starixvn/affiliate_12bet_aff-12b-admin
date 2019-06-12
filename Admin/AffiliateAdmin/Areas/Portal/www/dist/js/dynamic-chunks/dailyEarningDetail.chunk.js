(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["dailyEarningDetail"],{

/***/ "./Areas/Portal/www/source/js/modules/numberFormat.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/numberFormat.js ***!
  \************************************************************/
/*! exports provided: formatDecimal, format, formatDecimalWithCurrencySymbol, round */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "formatDecimal", function() { return formatDecimal; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "format", function() { return format; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "formatDecimalWithCurrencySymbol", function() { return formatDecimalWithCurrencySymbol; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "round", function() { return round; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__);

function formatDecimal(value) {
  return format(value, 2);
}
function format(value) {
  var digits = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : 0;

  if (typeof value !== "number" || isNaN(value)) {
    return "";
  }

  var roundedValue = round(value, digits);
  return roundedValue.toFixed(digits).replace(/(\d)(?=(\d{3})+(\.|$))/g, "$1,");
}
function formatDecimalWithCurrencySymbol(value) {
  var _context, _context2;

  var currencySymbol = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : "$";
  return value < 0 ? _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context = "-".concat(currencySymbol)).call(_context, formatDecimal(Math.abs(value))) : _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context2 = "".concat(currencySymbol)).call(_context2, formatDecimal(value));
}
function round(value, digits) {
  var sign = value < 0 ? -1 : 1;
  var absoluteValue = Math.abs(value);
  var rounded = Number(Math.round(absoluteValue + 'e' + digits) + 'e-' + digits);
  return sign * rounded;
}

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/components/UserPanelBreadcrumb.jsx":
/*!************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/components/UserPanelBreadcrumb.jsx ***!
  \************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");



var UserPanelBreadcrumb = function UserPanelBreadcrumb(props) {
  function _renderBreadcrumbItem(breadcrumbList) {
    var arrayItem = [];
    var lastIndex = breadcrumbList.length - 1;

    for (var i = 0; i < lastIndex; i++) {
      arrayItem.push(react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["BreadcrumbItem"], null, react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("a", {
        href: "javascript:;",
        onClick: breadcrumbList[i].handleClick
      }, breadcrumbList[i].title)));
    }

    arrayItem.push(react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["BreadcrumbItem"], {
      active: true
    }, breadcrumbList[lastIndex].title));
    return arrayItem;
  }

  var breadcrumbList = props.breadcrumbList || null;
  return breadcrumbList && react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Breadcrumb"], null, _renderBreadcrumbItem(breadcrumbList));
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(UserPanelBreadcrumb));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/AppContainer.jsx":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/AppContainer.jsx ***!
  \******************************************************************************************************/
/*! exports provided: AppContainer, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppContainer", function() { return AppContainer; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _common_components_UserPanelBreadcrumb__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../../../common/components/UserPanelBreadcrumb */ "./Areas/Portal/www/source/js/pages/common/components/UserPanelBreadcrumb.jsx");
/* harmony import */ var _Filter__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/Filter.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/Content.jsx");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/actions/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/modules/constants.js");
















var AppContainer =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6___default()(AppContainer, _PureComponent);

  function AppContainer(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default()(this, AppContainer);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(AppContainer).call(this, props));
    var _this$props = _this.props,
        param = _this$props.param,
        affId = _this$props.affId;
    _this.affId = affId;
    _this.monthDate = param && param.monthDate;
    _this.date = new Date(_this.monthDate);
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default()(AppContainer, [{
    key: "render",
    value: function render() {
      var _this2 = this,
          _context;

      var param = this.props.param;
      var dailyEarningParam = {
        affId: this.affId,
        monthDate: this.monthDate,
        productId: param.productId
      };
      var breadcrumbList = [{
        handleClick: function handleClick() {
          return _this2.props.setSelectedUserPanel(_modules_constants__WEBPACK_IMPORTED_MODULE_15__["userPanelMenuKey"].statementHistory);
        },
        title: 'Statement History'
      }, {
        handleClick: function handleClick() {
          return _this2.props.redirectToDailyEarning(dailyEarningParam);
        },
        title: _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context = "".concat(_modules_constants__WEBPACK_IMPORTED_MODULE_15__["monthNames"][this.date.getMonth()], " ")).call(_context, this.date.getFullYear())
      }, {
        title: this.monthDate
      }];
      return this.monthDate && react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_10__["default"], null, react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_common_components_UserPanelBreadcrumb__WEBPACK_IMPORTED_MODULE_11__["default"], {
        breadcrumbList: breadcrumbList
      }), react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_Filter__WEBPACK_IMPORTED_MODULE_12__["default"], null), react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_13__["default"], null));
    }
  }]);

  return AppContainer;
}(react__WEBPACK_IMPORTED_MODULE_7__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      dailyEarningDetail = _ref.dailyEarningDetail;
  var param = dailyEarningDetail.param;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    param: param,
    affId: affId
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_9__["bindActionCreators"])({
    setSelectedUserPanel: _actions__WEBPACK_IMPORTED_MODULE_14__["setSelectedUserPanel"],
    redirectToDailyEarning: _actions__WEBPACK_IMPORTED_MODULE_14__["redirectToDailyEarning"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_8__["connect"])(mapStateToProps, mapDispatchToProps)(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/Content.jsx":
/*!*************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/Content.jsx ***!
  \*************************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2__);
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
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/actions/index.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");
















var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Content).call(this, props));
    _this.state = {
      gridData: null,
      total: null,
      isSelectedAllProducts: true
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var param = this.props.param;

      if (param !== prevProps.param) {
        this.fetchData(param);
      }
    }
  }, {
    key: "fetchData",
    value: function () {
      var _fetchData = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.mark(function _callee(param) {
        var affId, postData, data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                this.props.toggleLoaderUserPanel(true);
                affId = this.props.affId;
                postData = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2___default()({}, param, {
                  affId: affId
                });
                _context.next = 5;
                return this.props.fetchData(postData);

              case 5:
                data = _context.sent;
                this.setState({
                  gridData: data ? data.MainData : null,
                  total: data ? data.Total : 0,
                  isSelectedAllProducts: param.ProductId === _modules_constants__WEBPACK_IMPORTED_MODULE_13__["selectedAllProduct"]
                });
                this.props.toggleLoaderUserPanel(false);

              case 8:
              case "end":
                return _context.stop();
            }
          }
        }, _callee, this);
      }));

      function fetchData(_x) {
        return _fetchData.apply(this, arguments);
      }

      return fetchData;
    }()
  }, {
    key: "_renderHeader",
    value: function _renderHeader() {
      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Row"], null, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 30
      }, "#"), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].UserName12Bet
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].UserName12Bet), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].IbcUserName
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].IbcUserName), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Winlost
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Winlost), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Deposit
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Deposit), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Withdrawal
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Withdrawal), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Bonus
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Bonus), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].BankingFee
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].BankingFee));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList, productColumnClassName) {
      var dataRows = [];
      var isTotalRow = dataList.length === 1 && !dataList[0].IbcUserName;

      if (isTotalRow) {
        dataList[0].IbcUserName = _modules_constants__WEBPACK_IMPORTED_MODULE_13__["resource"].Total;
      }

      for (var i = 0; i < dataList.length; i++) {
        var dataItem = dataList[i];
        dataRows.push(this._buildDataRow(dataItem, isTotalRow ? null : i + 1, productColumnClassName));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, rowNumber, productColumnClassName) {
      var winlost = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__["formatDecimal"])(dataItem.WinLost);
      var deposit = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__["formatDecimal"])(dataItem.Deposit);
      var withdrawal = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__["formatDecimal"])(dataItem.Withdrawal);
      var bonus = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__["formatDecimal"])(dataItem.Bonus);
      var bankingFee = Object(_modules_numberFormat__WEBPACK_IMPORTED_MODULE_15__["formatDecimal"])(dataItem.BankingFee);
      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Row"], null, rowNumber && react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-center",
        title: rowNumber
      }, rowNumber), rowNumber && react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-center",
        title: dataItem.UserName
      }, dataItem.UserName), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-center",
        title: dataItem.IbcUserName,
        colSpan: !rowNumber && 3
      }, dataItem.IbcUserName), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: productColumnClassName,
        title: winlost
      }, winlost), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-right",
        title: deposit
      }, deposit), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-right",
        title: withdrawal
      }, withdrawal), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-right",
        title: bonus
      }, bonus), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        className: "text-right",
        title: bankingFee
      }, bankingFee));
    }
  }, {
    key: "render",
    value: function render() {
      var _this$state = this.state,
          gridData = _this$state.gridData,
          total = _this$state.total,
          isSelectedAllProducts = _this$state.isSelectedAllProducts;
      var productColumnClassName = isSelectedAllProducts ? 'text-right' : 'text-right highlight';
      return gridData && total && react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
        className: "table-responsive table-striped my-2"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Table"], {
        minWidth: 1000,
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_13__["maxWidth"],
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(gridData, productColumnClassName),
        footer: this._renderDataSection([total], productColumnClassName),
        containerPadding: 10
      }));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_9__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      dailyEarningDetail = _ref.dailyEarningDetail;
  var param = dailyEarningDetail.param;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    affId: affId,
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_11__["bindActionCreators"])({
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_14__["toggleLoaderUserPanel"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_14__["fetchData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_10__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/Filter.jsx":
/*!************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/components/Filter.jsx ***!
  \************************************************************************************************/
/*! exports provided: Filter, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Filter", function() { return Filter; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/actions/index.js");


















var Filter =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_10___default()(Filter, _PureComponent);

  function Filter(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default()(this, Filter);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_7___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_8___default()(Filter).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9___default()(_this), "handleChangeProduct", function (event) {
      var selectedProduct = Number(event.target.value);

      var param = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_4___default()({}, _this.props.param, {
        productId: selectedProduct
      });

      _this.props.setParam(param);

      _this.setState({
        selectedProduct: selectedProduct
      });
    });

    var _param = _this.props.param;
    _this.state = {
      productList: [],
      selectedProduct: _param.productId || _modules_constants__WEBPACK_IMPORTED_MODULE_16__["selectedAllProduct"]
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default()(Filter, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      this._fetchProductList();

      this.props.setParam(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_4___default()({}, this.props.param, {
        productId: this.state.selectedProduct
      }));
    }
  }, {
    key: "_fetchProductList",
    value: function () {
      var _fetchProductList2 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.mark(function _callee() {
        var productList;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                _context.next = 2;
                return this.props.getProductList();

              case 2:
                productList = _context.sent;
                this.setState({
                  productList: productList
                });

              case 4:
              case "end":
                return _context.stop();
            }
          }
        }, _callee, this);
      }));

      function _fetchProductList() {
        return _fetchProductList2.apply(this, arguments);
      }

      return _fetchProductList;
    }()
  }, {
    key: "render",
    value: function render() {
      var _this$state = this.state,
          productList = _this$state.productList,
          selectedProduct = _this$state.selectedProduct;
      return react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("div", {
        className: "filter-container"
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Form"], {
        inline: true,
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Label"], {
        className: "mr-2"
      }, "Search By"), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Input"], {
        type: "select",
        name: "Product",
        id: "Product",
        value: selectedProduct,
        onChange: this.handleChangeProduct
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("option", {
        value: "0"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default()(productList).call(productList, function (p) {
        return react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("option", {
          value: p.ProductId
        }, p.ProductName);
      })))));
    }
  }]);

  return Filter;
}(react__WEBPACK_IMPORTED_MODULE_12__["PureComponent"]);

function mapStateToProps(_ref) {
  var dailyEarningDetail = _ref.dailyEarningDetail;
  var param = dailyEarningDetail.param;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_14__["bindActionCreators"])({
    setParam: _actions__WEBPACK_IMPORTED_MODULE_17__["setParam"],
    getProductList: _actions__WEBPACK_IMPORTED_MODULE_17__["getProductList"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_13__["connect"])(mapStateToProps, mapDispatchToProps)(Filter));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/modules/constants.js":
/*!***********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/daily-earning-detail/modules/constants.js ***!
  \***********************************************************************************************/
/*! exports provided: selectedAllProduct, globalResource, maxWidth, userPanelMenuKey, monthNames, resource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_0__["globalResource"]; });

/* harmony import */ var _customer_common_modules_constants__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../customer/common/modules/constants */ "./Areas/Portal/www/source/js/pages/customer/common/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "userPanelMenuKey", function() { return _customer_common_modules_constants__WEBPACK_IMPORTED_MODULE_1__["userPanelMenuKey"]; });

/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "selectedAllProduct", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_2__["selectedAllProduct"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "maxWidth", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_2__["maxWidth"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "monthNames", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_2__["monthNames"]; });





var resource = {
  UserName12Bet: '12BET User Name',
  IbcUserName: 'IBC User Name',
  Winlost: 'Winlost',
  Deposit: 'Deposit',
  Withdrawal: 'Withdrawal',
  Bonus: 'Bonus',
  BankingFee: 'Banking Fee',
  Total: 'Total'
};

/***/ })

}]);
//# sourceMappingURL=dailyEarningDetail.chunk.js.map