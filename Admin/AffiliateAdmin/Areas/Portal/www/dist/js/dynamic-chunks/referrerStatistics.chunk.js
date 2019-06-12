(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["referrerStatistics"],{

/***/ "./Areas/Portal/www/source/js/pages/common/components/UserPanelSubMenu.jsx":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/components/UserPanelSubMenu.jsx ***!
  \*********************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");




var UserPanelSubMenu = function UserPanelSubMenu(props) {
  var subMenuList = props.subMenuList || null;
  return subMenuList && react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("div", {
    className: "sub-menu-container pt-1"
  }, react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["Nav"], null, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(subMenuList).call(subMenuList, function (item) {
    return react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["NavItem"], null, react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["NavLink"], {
      active: item.isActive,
      href: "javascript:;",
      onClick: item.handleClick
    }, item.title));
  })));
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_1___default.a.memo(UserPanelSubMenu));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/customer/user-panel/modules/constants.js":
/*!***********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/customer/user-panel/modules/constants.js ***!
  \***********************************************************************************/
/*! exports provided: pcLaptop, mobile, deviceTypes */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "pcLaptop", function() { return pcLaptop; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "mobile", function() { return mobile; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deviceTypes", function() { return deviceTypes; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);


var _deviceTypes;

var pcLaptop = 'PC/Laptop';
var mobile = 'Mobile';
var deviceTypes = (_deviceTypes = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_deviceTypes, pcLaptop, 0), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_deviceTypes, mobile, 1), _deviceTypes);

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/AppContainer.jsx":
/*!*******************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/referrers/components/AppContainer.jsx ***!
  \*******************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Filter__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Filter.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Content.jsx");
/* harmony import */ var _Detail__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./Detail */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Detail.jsx");
/* harmony import */ var _common_components_UserPanelSubMenu__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../../../common/components/UserPanelSubMenu */ "./Areas/Portal/www/source/js/pages/common/components/UserPanelSubMenu.jsx");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/actions/index.js");















var AppContainer =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4___default()(AppContainer, _PureComponent);

  function AppContainer(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_0___default()(this, AppContainer);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_2___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_3___default()(AppContainer).call(this, props));
    _this.subMenuList = [{
      isActive: true,
      handleClick: function handleClick() {
        return _this.props.setSelectedUserPanel('clickStatistics');
      },
      title: 'Referrers'
    }, {
      isActive: false,
      handleClick: function handleClick() {
        return _this.props.setSelectedUserPanel('landingPageStatistics');
      },
      title: 'Landing Pages'
    }];
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_1___default()(AppContainer, [{
    key: "render",
    value: function render() {
      return react__WEBPACK_IMPORTED_MODULE_5___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_8__["default"], null, react__WEBPACK_IMPORTED_MODULE_5___default.a.createElement(_common_components_UserPanelSubMenu__WEBPACK_IMPORTED_MODULE_12__["default"], {
        subMenuList: this.subMenuList
      }), react__WEBPACK_IMPORTED_MODULE_5___default.a.createElement(_Filter__WEBPACK_IMPORTED_MODULE_9__["default"], null), react__WEBPACK_IMPORTED_MODULE_5___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_10__["default"], null), react__WEBPACK_IMPORTED_MODULE_5___default.a.createElement(_Detail__WEBPACK_IMPORTED_MODULE_11__["default"], null));
    }
  }]);

  return AppContainer;
}(react__WEBPACK_IMPORTED_MODULE_5__["PureComponent"]);

function mapStateToProps() {}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_7__["bindActionCreators"])({
    setSelectedUserPanel: _actions__WEBPACK_IMPORTED_MODULE_13__["setSelectedUserPanel"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_6__["connect"])(mapStateToProps, mapDispatchToProps)(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Content.jsx":
/*!**************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Content.jsx ***!
  \**************************************************************************************/
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/actions/index.js");
















var Content =
/*#__PURE__*/
function (_Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Content, _Component);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Content).call(this, props));
    _this.state = {
      gridData: null
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var param = this.props.param;

      if (param !== prevProps.param) {
        this.fetchData(this.props.param);
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
                  gridData: data
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
      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Row"], {
        key: "thead"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 580,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].SiteUrls
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].SiteUrls), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].TotalClick
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].TotalClick), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].UniqueClick
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].UniqueClick), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Signups
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Signups), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Fdm
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Fdm));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList) {
      var dataRows = [];

      if (dataList && dataList.length) {
        for (var i = 0; i < dataList.length; i++) {
          var dataItem = dataList[i];
          dataRows.push(this._buildDataRow(dataItem, i));
        }
      } else {
        dataRows.push(react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Row"], null, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
          className: "text-center",
          colSpan: "5"
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_14__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index) {
      var _this2 = this;

      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Row"], {
        key: "dataRow-".concat(index)
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        title: dataItem.SiteUrl
      }, dataItem.SiteUrl == "Old URL" || dataItem.SiteUrl == "none" ? dataItem.SiteUrl : react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Button"], {
        color: "link",
        className: "p-0",
        onClick: function onClick() {
          return _this2.props.setSelectedSiteUrl(dataItem.SiteUrl);
        }
      }, dataItem.SiteUrl)), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        title: dataItem.TotalClick,
        className: "text-right"
      }, dataItem.TotalClick), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        title: dataItem.UniqueClick,
        className: "text-right"
      }, dataItem.UniqueClick), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        title: dataItem.Signups,
        className: "text-right"
      }, dataItem.Signups), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Cell"], {
        title: dataItem.Fdm,
        className: "text-right"
      }, dataItem.Fdm));
    }
  }, {
    key: "render",
    value: function render() {
      var gridData = this.state.gridData;
      return gridData && react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
        className: "table-responsive table-striped my-2"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_12__["Table"], {
        minWidth: 1000,
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_14__["maxWidth"],
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(gridData),
        containerPadding: 10
      }));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_9__["Component"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      clickStatistics = _ref.clickStatistics;
  var param = clickStatistics.param;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    affId: affId,
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_11__["bindActionCreators"])({
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_15__["toggleLoaderUserPanel"],
    setSelectedSiteUrl: _actions__WEBPACK_IMPORTED_MODULE_15__["setSelectedSiteUrl"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_15__["fetchData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_10__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Detail.jsx":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Detail.jsx ***!
  \*************************************************************************************/
/*! exports provided: Detail, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Detail", function() { return Detail; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3__);
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
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__);
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/actions/index.js");

















var Detail =
/*#__PURE__*/
function (_Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(Detail, _Component);

  function Detail(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Detail);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Detail).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handlePaging", function (pageIndex, pageSize) {
      var newPaging = {
        PageIndex: pageIndex,
        PageSize: pageSize
      };

      _this.props.setPagingOption(newPaging);

      var _this$props = _this.props,
          param = _this$props.param,
          selectedSiteUrl = _this$props.selectedSiteUrl;

      var newParam = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default()({}, param, {
        selectedSiteUrl: selectedSiteUrl,
        pageIndex: pageIndex,
        pageSize: pageSize
      });

      _this.fetchData(newParam);
    });

    _this.state = {
      gridData: null
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Detail, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var _this$props2 = this.props,
          pagingOption = _this$props2.pagingOption,
          selectedSiteUrl = _this$props2.selectedSiteUrl,
          param = _this$props2.param;

      if (selectedSiteUrl && selectedSiteUrl !== prevProps.selectedSiteUrl) {
        var postParam = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default()({}, param, {
          selectedSiteUrl: selectedSiteUrl,
          PageIndex: 1,
          PageSize: pagingOption.PageSize
        });

        this.fetchData(postParam);
      }
    }
  }, {
    key: "fetchData",
    value: function () {
      var _fetchData = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.mark(function _callee(param) {
        var affId, postData, data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                this.props.toggleLoaderUserPanel(true);
                affId = this.props.affId;
                postData = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_3___default()({}, param, {
                  affId: affId
                });
                _context.next = 5;
                return this.props.fetchDetailData(postData);

              case 5:
                data = _context.sent;
                this.setState({
                  gridData: data ? data.MainData : null,
                  shouldResetScrollPosition: true
                });
                this.props.setPagingOption({
                  TotalItem: data ? data.TotalItem : 0,
                  PageIndex: param.pageIndex,
                  PageSize: param.PageSize
                });
                this.props.toggleLoaderUserPanel(false);

              case 9:
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
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "thead"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 580,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].WebsiteUrl
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].WebsiteUrl), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].TotalClicks
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].TotalClicks), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].UniqueClicks
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].UniqueClicks), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].Signup
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].Signup), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 110,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].Fdm
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["headerText"].Fdm));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList) {
      var dataRows = [];

      if (dataList && dataList.length) {
        for (var i = 0; i < dataList.length; i++) {
          var dataItem = dataList[i];
          dataRows.push(this._buildDataRow(dataItem, i));
        }
      } else {
        dataRows.push(react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
          className: "text-center",
          colSpan: "5"
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index) {
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "dataRow-".concat(index)
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.WebsiteUrl
      }, dataItem.WebsiteUrl), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.TotalClick,
        className: "text-right"
      }, dataItem.TotalClick), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.UniqueClick,
        className: "text-right"
      }, dataItem.UniqueClick), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.Signups,
        className: "text-right"
      }, dataItem.Signups), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.Fdm,
        className: "text-right"
      }, dataItem.Fdm));
    }
  }, {
    key: "render",
    value: function render() {
      var gridData = this.state.gridData;
      return this.props.selectedSiteUrl && react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "table-responsive table-striped my-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Table"], {
        minWidth: 1010,
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["maxWidth"],
        autoHeight: false,
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(gridData),
        containerPadding: 10,
        isPaging: true,
        pageOption: this.props.pagingOption,
        onPaging: this.handlePaging
      }));
    }
  }]);

  return Detail;
}(react__WEBPACK_IMPORTED_MODULE_11__["Component"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      clickStatistics = _ref.clickStatistics;
  var pagingOption = clickStatistics.pagingOption,
      param = clickStatistics.param,
      selectedSiteUrl = clickStatistics.selectedSiteUrl;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    affId: affId,
    pagingOption: pagingOption,
    selectedSiteUrl: selectedSiteUrl,
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_16__["toggleLoaderUserPanel"],
    setPagingOption: _actions__WEBPACK_IMPORTED_MODULE_16__["setPagingOption"],
    fetchDetailData: _actions__WEBPACK_IMPORTED_MODULE_16__["fetchDetailData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Detail));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Filter.jsx":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/referrers/components/Filter.jsx ***!
  \*************************************************************************************/
/*! exports provided: Filter, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Filter", function() { return Filter; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3__);
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @9softstudio/react-datepicker */ "./node_modules/@9softstudio/react-datepicker/dist/index.js");
/* harmony import */ var _9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15__);
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/actions/index.js");
/* harmony import */ var _modules_utils__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../../../../modules/utils */ "./Areas/Portal/www/source/js/modules/utils.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/referrers/modules/constants.js");



















var minDate = new Date(2009, 11, 1);
var Filter =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(Filter, _PureComponent);

  function Filter(props) {
    var _context, _context2, _context3;

    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Filter);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Filter).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChange", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, event.target.name, event.target.value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChangeFromDate", function (momentDate) {
      _this.setState(Object(_modules_utils__WEBPACK_IMPORTED_MODULE_17__["getDateArrange"])(momentDate, _this.state.toDate));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChangeToDate", function (momentDate) {
      _this.setState({
        toDate: momentDate.format("MM/DD/YYYY")
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleSearch", function (state) {
      _this.props.setParam(state);

      _this.props.setSelectedSiteUrl(null);
    });

    var _this$props = _this.props,
        today = _this$props.today,
        param = _this$props.param;
    var deviceType = param.deviceType,
        fromDate = param.fromDate,
        toDate = param.toDate;
    var now = new Date(today);
    _this.nowString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context2 = "".concat(now.getMonth() + 1, "/")).call(_context2, now.getDate(), "/")).call(_context, now.getFullYear());

    var firstDayOfCurrentMonth = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context3 = "".concat(now.getMonth() + 1, "/01/")).call(_context3, now.getFullYear());

    _this.state = {
      deviceType: param.isClickLink ? deviceType : _modules_constants__WEBPACK_IMPORTED_MODULE_18__["deviceTypes"][_modules_constants__WEBPACK_IMPORTED_MODULE_18__["pcLaptop"]],
      fromDate: param.isClickLink ? fromDate : firstDayOfCurrentMonth,
      toDate: param.isClickLink ? toDate : _this.nowString
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Filter, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      if (this.props.param.isClickLink) {
        this.handleSearch(this.state);
      }
    }
  }, {
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var param = this.props.param;

      if (param !== prevProps.param) {
        var newState = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
          deviceType: param.deviceType,
          fromDate: param.fromDate,
          toDate: param.toDate
        });

        this.setState(newState);
        this.props.setSelectedSiteUrl(null);

        if (param.isClickLink) {
          this.handleSearch(newState);
        }
      }
    }
  }, {
    key: "render",
    value: function render() {
      var _context4,
          _this2 = this;

      var _this$state = this.state,
          deviceType = _this$state.deviceType,
          fromDate = _this$state.fromDate,
          toDate = _this$state.toDate;
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "filter-container"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Form"], {
        inline: true,
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["globalResource"].From), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15__["DatePicker"], {
        minDate: minDate,
        maxDate: this.nowString,
        date: fromDate,
        iconElement: react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        }),
        onChange: this.handleChangeFromDate
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["globalResource"].To), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15__["DatePicker"], {
        minDate: fromDate,
        maxDate: this.nowString,
        date: toDate,
        iconElement: react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        }),
        onChange: this.handleChangeToDate
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mb-2 mr-sm-2 mb-sm-0"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "deviceType",
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["headerText"].Device), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "deviceType",
        id: "deviceType",
        value: deviceType,
        onChange: this.handleChange
      }, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default()(_context4 = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0___default()(_modules_constants__WEBPACK_IMPORTED_MODULE_18__["deviceTypes"])).call(_context4, function (key) {
        return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "deviceType-".concat(_modules_constants__WEBPACK_IMPORTED_MODULE_18__["deviceTypes"][key]),
          value: _modules_constants__WEBPACK_IMPORTED_MODULE_18__["deviceTypes"][key]
        }, key);
      }))), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        outline: true,
        color: "primary",
        type: "button",
        onClick: function onClick() {
          return _this2.handleSearch(_this2.state);
        }
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-magnifying-glass"
      }), " ", _modules_constants__WEBPACK_IMPORTED_MODULE_18__["globalResource"].Search)));
    }
  }]);

  return Filter;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref) {
  var today = _ref.today,
      clickStatistics = _ref.clickStatistics;
  var param = clickStatistics.param;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
    today: today,
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setSelectedSiteUrl: _actions__WEBPACK_IMPORTED_MODULE_16__["setSelectedSiteUrl"],
    setParam: _actions__WEBPACK_IMPORTED_MODULE_16__["setParam"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Filter));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/referrers/modules/constants.js":
/*!************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/referrers/modules/constants.js ***!
  \************************************************************************************/
/*! exports provided: maxWidth, globalResource, deviceTypes, pcLaptop, headerText */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "headerText", function() { return headerText; });
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "maxWidth", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_0__["maxWidth"]; });

/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"]; });

/* harmony import */ var _customer_user_panel_modules_constants__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../customer/user-panel/modules/constants */ "./Areas/Portal/www/source/js/pages/customer/user-panel/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "deviceTypes", function() { return _customer_user_panel_modules_constants__WEBPACK_IMPORTED_MODULE_2__["deviceTypes"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "pcLaptop", function() { return _customer_user_panel_modules_constants__WEBPACK_IMPORTED_MODULE_2__["pcLaptop"]; });





var headerText = {
  SiteUrls: 'Site URLs',
  TotalClick: 'Total Click',
  UniqueClick: 'Unique Click',
  Signups: 'Signups',
  Fdm: 'FDM',
  Device: 'Device',
  WebsiteUrl: 'Website URL',
  TotalClicks: 'Total Clicks',
  UniqueClicks: 'Unique Clicks',
  Signup: 'Sign up'
};

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js":
/*!***************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/keys */ "./node_modules/core-js-pure/stable/object/keys.js");

/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/keys.js":
/*!*********************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/keys.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! ../../es/object/keys */ "./node_modules/core-js-pure/es/object/keys.js");


/***/ }),

/***/ "./node_modules/moment/moment.js":
/*!*******************************************************************************************************!*\
  !*** delegated ../../../node_modules/moment/moment.js from dll-reference moment_b4d98fc51f6a4237233c ***!
  \*******************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference moment_b4d98fc51f6a4237233c */ "dll-reference moment_b4d98fc51f6a4237233c"))(178);

/***/ })

}]);
//# sourceMappingURL=referrerStatistics.chunk.js.map