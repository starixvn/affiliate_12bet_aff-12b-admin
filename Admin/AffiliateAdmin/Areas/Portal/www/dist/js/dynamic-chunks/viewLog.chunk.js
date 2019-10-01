(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["viewLog"],{

/***/ "./Areas/Portal/www/source/js/pages/user-panel/view-log/actions/index.js":
/*!*******************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/view-log/actions/index.js ***!
  \*******************************************************************************/
/*! exports provided: setPagingOption, toggleLoaderUserPanel, setParam, setShowIdList, fetchData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setParam", function() { return setParam; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setShowIdList", function() { return setShowIdList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "fetchData", function() { return fetchData; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _actions_pagingAction__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../actions/pagingAction */ "./Areas/Portal/www/source/js/actions/pagingAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "setPagingOption", function() { return _actions_pagingAction__WEBPACK_IMPORTED_MODULE_3__["setPagingOption"]; });

/* harmony import */ var _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../customer/user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "toggleLoaderUserPanel", function() { return _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["toggleLoaderUserPanel"]; });

/* harmony import */ var _actions_types__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../actions/types */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/actions/types.js");







var setParam = function setParam(data) {
  return {
    type: _actions_types__WEBPACK_IMPORTED_MODULE_5__["SET_PARAM"],
    data: data
  };
};
var setShowIdList = function setShowIdList(data) {
  return {
    type: _actions_types__WEBPACK_IMPORTED_MODULE_5__["SET_SHOW_ID_LIST"],
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
              url = 'Portal/UserPanel/ViewLog/GetViewLogList';
              _context.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

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

/***/ "./Areas/Portal/www/source/js/pages/user-panel/view-log/components/AppContainer.jsx":
/*!******************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/view-log/components/AppContainer.jsx ***!
  \******************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Filter__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/components/Filter.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/components/Content.jsx");





var AppContainer = function AppContainer(props) {
  return react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Filter__WEBPACK_IMPORTED_MODULE_2__["default"], null), react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_3__["default"], null));
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/view-log/components/Content.jsx":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/view-log/components/Content.jsx ***!
  \*************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3__);
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/actions/index.js");



















var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Content).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleShow", function (id) {
      var showIdList = _this.props.showIdList;

      _this.setState({
        shouldResetScrollPosition: false
      });

      _this.props.setShowIdList(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, showIdList, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, id, !showIdList[id])));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handlePaging", function (pageIndex, pageSize) {
      var newParam = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.props.param, {
        pageIndex: pageIndex,
        pageSize: pageSize
      });

      _this.fetchData(newParam);

      _this.props.setShowIdList({});
    });

    _this.state = {
      gridData: null,
      shouldResetScrollPosition: true
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var _this$props = this.props,
          pagingOption = _this$props.pagingOption,
          param = _this$props.param;

      if (param !== prevProps.param) {
        var newParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, param, {
          pageIndex: 1,
          pageSize: pagingOption.PageSize
        });

        this.fetchData(newParam);
      }
    }
  }, {
    key: "fetchData",
    value: function () {
      var _fetchData = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(param) {
        var affId, postData, data;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
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
                  shouldResetScrollPosition: true
                });
                this.props.setPagingOption({
                  TotalItem: data ? data.TotalItem : 0,
                  PageIndex: param.pageIndex,
                  PageSize: param.pageSize
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
        colWidth: 35,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Index
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Index), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 70,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Id
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Id), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 125,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Date
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Date), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].UpdatedBy
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].UpdatedBy), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 130,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Action
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Action), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 440,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Description
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Description), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 100,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Ip
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Ip));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList) {
      var dataRows = [];

      if (dataList && dataList.length) {
        var pagingOption = this.props.pagingOption;
        var startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (var i = 0; i < dataList.length; i++) {
          var isLink = false;
          var isHide = false;
          var dataItem = dataList[i];
          var dataPreviousItem = i == 0 ? {} : dataList[i - 1];
          var dataNextItem = i == dataList.length - 1 ? {} : dataList[i + 1];

          if (dataItem.Id == dataPreviousItem.Id) {
            isHide = true;
          } else if (dataItem.Id == dataNextItem.Id) {
            isLink = true;
          }

          dataRows.push(this._buildDataRow(dataItem, i + startIndex, isLink, isHide));
        }
      } else {
        dataRows.push(react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
          className: "text-center",
          colSpan: "7"
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index, isLink, isHide) {
      var _this2 = this;

      var id = dataItem.Id;
      var ipAddress = dataItem.Ip;
      var url = "http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=".concat(ipAddress, "&j=1");

      if (isHide && !this.props.showIdList[id]) {
        return null;
      }

      var updatedBy = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__["htmlDecode"])(dataItem.UpdatedBy);
      var action = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__["htmlDecode"])(dataItem.Action);
      var description = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__["htmlDecode"])(dataItem.Description);
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "dataRow-".concat(index)
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: index,
        className: "text-center"
      }, index), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: id,
        className: "text-center"
      }, isLink ? react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
        color: "link",
        onClick: function onClick() {
          return _this2.handleShow(id);
        }
      }, id) : id), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.DateText,
        className: "text-center"
      }, dataItem.DateText), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: updatedBy,
        className: "text-center"
      }, updatedBy), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: action,
        className: "text-center"
      }, action), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: description,
        className: "text-break"
      }, description), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: ipAddress,
        className: "text-center"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("a", {
        target: "_blank",
        href: url
      }, ipAddress)));
    }
  }, {
    key: "render",
    value: function render() {
      var _this$state = this.state,
          gridData = _this$state.gridData,
          shouldResetScrollPosition = _this$state.shouldResetScrollPosition;
      return gridData && react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "data-content"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "table-responsive table-striped my-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Table"], {
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["maxWidth"],
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(gridData),
        containerPadding: 10,
        isPaging: true,
        pageOption: this.props.pagingOption,
        onPaging: this.handlePaging,
        shouldResetScrollPosition: shouldResetScrollPosition
      })));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      viewLog = _ref.viewLog;
  var pagingOption = viewLog.pagingOption,
      param = viewLog.param,
      showIdList = viewLog.showIdList;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
    affId: affId,
    pagingOption: pagingOption,
    showIdList: showIdList,
    param: param
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setPagingOption: _actions__WEBPACK_IMPORTED_MODULE_18__["setPagingOption"],
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_18__["toggleLoaderUserPanel"],
    setShowIdList: _actions__WEBPACK_IMPORTED_MODULE_18__["setShowIdList"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_18__["fetchData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/view-log/components/Filter.jsx":
/*!************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/view-log/components/Filter.jsx ***!
  \************************************************************************************/
/*! exports provided: Filter, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Filter", function() { return Filter; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2__);
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
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/view-log/actions/index.js");
/* harmony import */ var _modules_utils__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../../../../modules/utils */ "./Areas/Portal/www/source/js/modules/utils.js");



















var minDate = new Date(2009, 11, 1);
var Filter =
/*#__PURE__*/
function (_React$Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(Filter, _React$Component);

  function Filter(props) {
    var _context, _context2, _context3, _context4;

    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Filter);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Filter).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChange", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, event.target.name, event.target.value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChangeFromDate", function (momentDate) {
      _this.setState(Object(_modules_utils__WEBPACK_IMPORTED_MODULE_18__["getDateRange"])(momentDate, _this.state.toDate));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChangeToDate", function (momentDate) {
      _this.setState({
        toDate: momentDate.format("MM/DD/YYYY")
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleSearch", function () {
      _this.props.setParam(_this.state);

      _this.props.setShowIdList({});
    });

    var today = _this.props.today;
    var now = new Date(today);
    _this.nowString = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context2 = "".concat(now.getMonth() + 1, "/")).call(_context2, now.getDate(), "/")).call(_context, now.getFullYear());
    var sixMonthAgo = new Date(today);
    sixMonthAgo.setMonth(sixMonthAgo.getMonth() - 6);
    _this.state = {
      logType: 0,
      fromDate: _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context3 = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_3___default()(_context4 = "".concat(sixMonthAgo.getMonth() + 1, "/")).call(_context4, sixMonthAgo.getDate(), "/")).call(_context3, sixMonthAgo.getFullYear()),
      toDate: _this.nowString
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Filter, [{
    key: "render",
    value: function render() {
      var _context5;

      var _this$state = this.state,
          logType = _this$state.logType,
          fromDate = _this$state.fromDate,
          toDate = _this$state.toDate;
      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "filter-container"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Form"], {
        inline: true,
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "logType",
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Type), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "logType",
        id: "logType",
        value: logType,
        onChange: this.handleChange
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        value: "0"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(_context5 = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default()(_modules_constants__WEBPACK_IMPORTED_MODULE_16__["logTypes"])).call(_context5, function (index) {
        return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "logType-".concat(index),
          value: index
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["logTypes"][index]);
      }))), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].From), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15__["DatePicker"], {
        minDate: minDate,
        maxDate: this.nowString,
        date: fromDate,
        iconElement: react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        }),
        onChange: this.handleChangeFromDate
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        className: "mr-sm-2"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].To), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_datepicker__WEBPACK_IMPORTED_MODULE_15__["DatePicker"], {
        minDate: fromDate,
        maxDate: this.nowString,
        date: toDate,
        iconElement: react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
          className: "oi oi-calendar icon-calendar ml-1"
        }),
        onChange: this.handleChangeToDate
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        outline: true,
        color: "primary",
        type: "button",
        onClick: this.handleSearch
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-magnifying-glass"
      }), " ", _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].Search)));
    }
  }]);

  return Filter;
}(react__WEBPACK_IMPORTED_MODULE_11___default.a.Component);

function mapStateToProps(_ref) {
  var today = _ref.today;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    today: today
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setParam: _actions__WEBPACK_IMPORTED_MODULE_17__["setParam"],
    setShowIdList: _actions__WEBPACK_IMPORTED_MODULE_17__["setShowIdList"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Filter));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/view-log/modules/constants.js":
/*!***********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/view-log/modules/constants.js ***!
  \***********************************************************************************/
/*! exports provided: globalResource, maxWidth, logTypes, headerText */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "logTypes", function() { return logTypes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "headerText", function() { return headerText; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "maxWidth", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_1__["maxWidth"]; });

/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_2__["globalResource"]; });



var _logTypes;




var COMMISSION_GROUP = 1;
var USER_INFO = 2;
var TRANSACTION = 3;
var PAYOUT = 4;
var PERMISSION = 5;
var PLAYER = 6;
var BANNER = 7;
var EMAIL = 8;
var MESSAGE = 9;
var EXCHANGE_RATE = 10;
var USER_NOTE = 11;
var LOGIN_ADMIN = 12;
var LOGIN_AFFILIATE = 13;
var LOGOUT_ADMIN = 14;
var LOGOUT_AFFILIATE = 15;
var PASSWORD = 16;
var ADJUSTMENT = 17;
var MEMBER = 18;
var USER_CONTROL = 19;
var logTypes = (_logTypes = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, COMMISSION_GROUP, 'CommissionGroup'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, USER_INFO, 'UserInfo'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, TRANSACTION, 'Transaction'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, PAYOUT, 'Payout'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, PLAYER, 'Player'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, BANNER, 'Banner'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, EMAIL, 'Email'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, USER_NOTE, 'UserNote'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, LOGIN_AFFILIATE, 'LoginAffiliate'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, LOGOUT_AFFILIATE, 'LogoutAffiliate'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, PASSWORD, 'Password'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, ADJUSTMENT, 'Adjustment'), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_logTypes, MEMBER, 'Member'), _logTypes);
var headerText = {
  Type: 'Type',
  Index: '#',
  Id: 'ID',
  Date: 'Date',
  UpdatedBy: 'Updated By',
  Action: 'Action',
  Description: 'Description',
  Ip: 'IP'
};

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
//# sourceMappingURL=viewLog.chunk.js.map