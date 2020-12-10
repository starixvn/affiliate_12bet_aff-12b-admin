(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["note"],{

/***/ "./Areas/Portal/www/source/js/pages/user-panel/note/actions/index.js":
/*!***************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/note/actions/index.js ***!
  \***************************************************************************/
/*! exports provided: toggleLoaderUserPanel, setNoteList, getNoteList, setShowPopup, setNoteInfo, getNoteContentById, insertNote, updateNote */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setNoteList", function() { return setNoteList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getNoteList", function() { return getNoteList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setShowPopup", function() { return setShowPopup; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setNoteInfo", function() { return setNoteInfo; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getNoteContentById", function() { return getNoteContentById; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "insertNote", function() { return insertNote; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "updateNote", function() { return updateNote; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _actions_pagingAction__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../actions/pagingAction */ "./Areas/Portal/www/source/js/actions/pagingAction.js");
/* harmony import */ var _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../customer/user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "toggleLoaderUserPanel", function() { return _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["toggleLoaderUserPanel"]; });

/* harmony import */ var _types__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./types */ "./Areas/Portal/www/source/js/pages/user-panel/note/actions/types.js");







var setNoteList = function setNoteList(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_5__["SET_NOTE_LIST"],
    data: data
  };
};
var getNoteList = function getNoteList(param) {
  return /*#__PURE__*/function () {
    var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(dispatch) {
      var url, response, _response$data, MainData, TotalItem, pageIndex, pageSize, pagingOption;

      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              dispatch(Object(_customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["toggleLoaderUserPanel"])(true));
              url = 'Portal/UserPanel/Note/GetNoteList';
              _context.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 4:
              response = _context.sent;

              if (response && response.status === 200) {
                _response$data = response.data, MainData = _response$data.MainData, TotalItem = _response$data.TotalItem;
                dispatch(setNoteList(MainData));
                pageIndex = param.pageIndex, pageSize = param.pageSize;
                pagingOption = {
                  PageIndex: pageIndex,
                  PageSize: pageSize,
                  TotalItem: TotalItem
                };
                dispatch(Object(_actions_pagingAction__WEBPACK_IMPORTED_MODULE_3__["setPagingOption"])(pagingOption));
              }

              dispatch(Object(_customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_4__["toggleLoaderUserPanel"])(false));

            case 7:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    }));

    return function (_x) {
      return _ref.apply(this, arguments);
    };
  }();
};
var setShowPopup = function setShowPopup(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_5__["SET_SHOW_POPUP"],
    data: data
  };
};
var setNoteInfo = function setNoteInfo(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_5__["SET_NOTE_INFO"],
    data: data
  };
};
var getNoteContentById = function getNoteContentById(param) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2() {
    var url, response;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee2$(_context2) {
      while (1) {
        switch (_context2.prev = _context2.next) {
          case 0:
            url = 'Portal/UserPanel/Note/GetNoteContentById';
            _context2.next = 3;
            return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

          case 3:
            response = _context2.sent;
            return _context2.abrupt("return", response && response.status === 200 ? response.data : null);

          case 5:
          case "end":
            return _context2.stop();
        }
      }
    }, _callee2);
  }));
};
var insertNote = function insertNote(param) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee3() {
    var url, response;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee3$(_context3) {
      while (1) {
        switch (_context3.prev = _context3.next) {
          case 0:
            url = 'Portal/UserPanel/Note/InsertNote';
            _context3.next = 3;
            return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

          case 3:
            response = _context3.sent;
            return _context3.abrupt("return", response && response.status === 200 ? response.data : null);

          case 5:
          case "end":
            return _context3.stop();
        }
      }
    }, _callee3);
  }));
};
var updateNote = function updateNote(param) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee4() {
    var url, response;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee4$(_context4) {
      while (1) {
        switch (_context4.prev = _context4.next) {
          case 0:
            url = 'Portal/UserPanel/Note/UpdateNote';
            _context4.next = 3;
            return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

          case 3:
            response = _context4.sent;
            return _context4.abrupt("return", response && response.status === 200 ? response.data : null);

          case 5:
          case "end":
            return _context4.stop();
        }
      }
    }, _callee4);
  }));
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/note/components/AppContainer.jsx":
/*!**************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/note/components/AppContainer.jsx ***!
  \**************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/note/components/Content.jsx");
/* harmony import */ var _InsertUpdateNoteForm__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./InsertUpdateNoteForm */ "./Areas/Portal/www/source/js/pages/user-panel/note/components/InsertUpdateNoteForm.jsx");





var AppContainer = function AppContainer(props) {
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_2__["default"], null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_InsertUpdateNoteForm__WEBPACK_IMPORTED_MODULE_3__["default"], null));
};

/* harmony default export */ __webpack_exports__["default"] = (/*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/note/components/Content.jsx":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/note/components/Content.jsx ***!
  \*********************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9__);
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
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/note/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/note/actions/index.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ../../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");












function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }










var Content = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(Content, _PureComponent);

  var _super = _createSuper(Content);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_hasEditPermission", function () {
      var claimsPrincipal = _this.props.claimsPrincipal;
      return claimsPrincipal.hasWriteClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_19__["claimResource"].Note);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleShowPopup", function (noteId) {
      if (noteId === 0) {
        _this.props.setNoteInfo({
          noteId: noteId,
          noteContent: ''
        });
      } else {
        _this.getNoteContentById(noteId);
      }

      _this.props.setShowPopup(true);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handlePaging", function (pageIndex, pageSize) {
      var newParam = {
        affId: _this.props.affId,
        pageIndex: pageIndex,
        pageSize: pageSize
      };

      _this.props.getNoteList(newParam);
    });

    var pagingOption = props.pagingOption,
        affId = props.affId;

    var param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
      affId: affId,
      pageIndex: 1,
      pageSize: pagingOption.PageSize
    });

    _this.props.getNoteList(param);

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "_renderHeader",
    value: function _renderHeader() {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "thead"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 35,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Index
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Index), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 580,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Content
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].Content), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 150,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].CreatedBy
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].CreatedBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 200,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].LastModify
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["headerText"].LastModify), this._hasEditPermission() && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        header: true,
        colWidth: 35,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["globalResource"].Edit
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["globalResource"].Edit));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList) {
      var dataRows = [];

      if (dataList.length) {
        var pagingOption = this.props.pagingOption;
        var startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (var i = 0; i < dataList.length; i++) {
          var dataItem = dataList[i];
          dataRows.push(this._buildDataRow(dataItem, i + startIndex));
        }
      } else {
        dataRows.push( /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
          className: "text-center",
          colSpan: "5"
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index) {
      var _this2 = this;

      var noteContent = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__["htmlDecode"])(dataItem.NoteContent);
      var createdBy = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__["htmlDecode"])(dataItem.CreatedBy);
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Row"], {
        key: "dataRow-".concat(index)
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: index,
        className: "text-center"
      }, index), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: noteContent
      }, noteContent), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: createdBy
      }, createdBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: dataItem.LastModifyText,
        className: "text-center"
      }, dataItem.LastModifyText), this._hasEditPermission() && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Cell"], {
        title: "Edit Note",
        className: "text-center"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
        className: "p-0",
        color: "link",
        onClick: function onClick() {
          return _this2.handleShowPopup(dataItem.NoteId);
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-pencil text-secondary"
      }))));
    }
  }, {
    key: "getNoteContentById",
    value: function () {
      var _getNoteContentById = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.mark(function _callee(noteId) {
        var affId, data, NoteId, NoteContent, noteItem;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                this.props.toggleLoaderUserPanel(true);
                affId = this.props.affId;
                _context.next = 4;
                return this.props.getNoteContentById({
                  affId: affId,
                  noteId: noteId
                });

              case 4:
                data = _context.sent;

                if (data) {
                  NoteId = data.NoteId, NoteContent = data.NoteContent;
                  noteItem = {
                    noteId: NoteId,
                    noteContent: NoteContent
                  };
                  this.props.setNoteInfo(noteItem);
                }

                this.props.toggleLoaderUserPanel(false);

              case 7:
              case "end":
                return _context.stop();
            }
          }
        }, _callee, this);
      }));

      function getNoteContentById(_x) {
        return _getNoteContentById.apply(this, arguments);
      }

      return getNoteContentById;
    }()
  }, {
    key: "render",
    value: function render() {
      var _this3 = this;

      var noteList = this.props.noteList;
      return noteList && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "data-content"
      }, this._hasEditPermission() && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "my-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
        outline: true,
        type: "button",
        onClick: function onClick() {
          return _this3.handleShowPopup(0);
        }
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_17__["globalResource"].Add, " ", /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-plus ml-2"
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "table-responsive table-striped"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_14__["Table"], {
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["maxWidth"],
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(noteList),
        containerPadding: 10,
        isPaging: true,
        pageOption: this.props.pagingOption,
        onPaging: this.handlePaging
      })));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      note = _ref.note,
      claimsPrincipal = _ref.claimsPrincipal;
  var pagingOption = note.pagingOption,
      noteList = note.noteList,
      noteInfo = note.noteInfo;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
    affId: affId,
    pagingOption: pagingOption,
    noteList: noteList,
    noteInfo: noteInfo,
    claimsPrincipal: claimsPrincipal
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setShowPopup: _actions__WEBPACK_IMPORTED_MODULE_18__["setShowPopup"],
    setNoteInfo: _actions__WEBPACK_IMPORTED_MODULE_18__["setNoteInfo"],
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_18__["toggleLoaderUserPanel"],
    getNoteList: _actions__WEBPACK_IMPORTED_MODULE_18__["getNoteList"],
    getNoteContentById: _actions__WEBPACK_IMPORTED_MODULE_18__["getNoteContentById"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/note/components/InsertUpdateNoteForm.jsx":
/*!**********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/note/components/InsertUpdateNoteForm.jsx ***!
  \**********************************************************************************************/
/*! exports provided: InsertUpdateNoteForm, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "InsertUpdateNoteForm", function() { return InsertUpdateNoteForm; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/define-property */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/define-property.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/define-properties */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/define-properties.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptors */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptors.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/for-each */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/for-each.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptor */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptor.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/filter */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/filter.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/get-own-property-symbols */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-symbols.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_15___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_15__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_16___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_16__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_17___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_17__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_19___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_19__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/note/actions/index.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/note/modules/constants.js");
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");




















function ownKeys(object, enumerableOnly) { var keys = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_7___default()(object); if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default.a) { var symbols = _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default()(object); if (enumerableOnly) symbols = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_5___default()(symbols).call(symbols, function (sym) { return _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { var _context2; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context2 = ownKeys(Object(source), true)).call(_context2, function (key) { _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()(target, key, source[key]); }); } else if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default.a) { _babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1___default()(target, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default()(source)); } else { var _context3; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context3 = ownKeys(Object(source))).call(_context3, function (key) { _babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0___default()(target, key, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(source, key)); }); } } return target; }

function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_17___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_17___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_16___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_8___default()(Date, [], function () {})); return true; } catch (e) { return false; } }









var InsertUpdateNoteForm = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_15___default()(InsertUpdateNoteForm, _PureComponent);

  var _super = _createSuper(InsertUpdateNoteForm);

  function InsertUpdateNoteForm(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_12___default()(this, InsertUpdateNoteForm);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14___default()(_this), "handleChange", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()({}, event.target.name, event.target.value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14___default()(_this), "_validateNoteContent", function () {
      var noteContent = _this.state.noteContent;
      var result = {
        isValid: !!noteContent && noteContent.length <= _modules_constants__WEBPACK_IMPORTED_MODULE_25__["maxLength"],
        message: ''
      };

      if (!result.isValid) {
        result.message = noteContent ? _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].MaxLengthErrorMessage : _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].RequiredErrorMessage;
      }

      return result;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14___default()(_this), "handleSave", function (isValid) {
      _this.setState({
        isClickedSubmit: true
      });

      if (isValid) {
        var newNoteInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default()({}, _this.props.noteInfo, {
          noteContent: _this.state.noteContent
        });

        _this.Save(newNoteInfo);
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14___default()(_this), "hideMessage", function () {
      _this.setState({
        isShowMessage: false
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_18___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_14___default()(_this), "handleClosePopup", function () {
      _this.props.setShowPopup(false);

      _this.setState({
        noteContent: '',
        isClickedSubmit: false
      });
    });

    _this.state = {
      isClickedSubmit: false,
      noteContent: '',
      isShowMessage: false,
      isSuccess: true,
      message: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_13___default()(InsertUpdateNoteForm, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var noteInfo = this.props.noteInfo;

      if (noteInfo && noteInfo !== prevProps.noteInfo) {
        this.setState({
          noteContent: noteInfo.noteContent
        });
      }
    }
  }, {
    key: "Save",
    value: function () {
      var _Save = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_10___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_9___default.a.mark(function _callee(noteInfo) {
        var _this2 = this;

        var isInsert, message, affId, param, result, pagingOption, _param;

        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_9___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                isInsert = noteInfo.noteId === 0;
                message = isInsert ? _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].AddNoteSuccess : _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].UpdateNoteSuccess;
                affId = this.props.affId;
                param = _objectSpread(_objectSpread({}, noteInfo), {}, {
                  affId: affId
                });

                if (!isInsert) {
                  _context.next = 10;
                  break;
                }

                _context.next = 7;
                return this.props.insertNote(param);

              case 7:
                _context.t0 = _context.sent;
                _context.next = 13;
                break;

              case 10:
                _context.next = 12;
                return this.props.updateNote(param);

              case 12:
                _context.t0 = _context.sent;

              case 13:
                result = _context.t0;

                if (result) {
                  this.setState({
                    isSuccess: result.IsSuccessful,
                    isShowMessage: true,
                    message: result.IsSuccessful ? message : result.Message
                  }, function () {
                    return window.setTimeout(_this2.hideMessage, 3000);
                  });

                  if (result.IsSuccessful) {
                    pagingOption = this.props.pagingOption;
                    _param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default()({}, {
                      affId: affId,
                      pageIndex: 1,
                      pageSize: pagingOption.PageSize
                    });
                    window.setTimeout(function () {
                      return _this2.props.setShowPopup(false);
                    }, 3000);
                    window.setTimeout(function () {
                      return _this2.props.getNoteList(_param);
                    }, 3200);
                  }
                } else {
                  this.setState({
                    isSuccess: false,
                    isShowMessage: true,
                    message: _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].Failed
                  }, function () {
                    return window.setTimeout(_this2.hideMessage, 3000);
                  });
                }

              case 15:
              case "end":
                return _context.stop();
            }
          }
        }, _callee, this);
      }));

      function Save(_x) {
        return _Save.apply(this, arguments);
      }

      return Save;
    }()
  }, {
    key: "render",
    value: function render() {
      var _this3 = this;

      var _this$state = this.state,
          isShowMessage = _this$state.isShowMessage,
          isSuccess = _this$state.isSuccess,
          message = _this$state.message,
          isClickedSubmit = _this$state.isClickedSubmit;
      var _this$props = this.props,
          isShowPopup = _this$props.isShowPopup,
          noteInfo = _this$props.noteInfo;
      var isInsert = noteInfo.noteId === 0;
      var popupTitle = isInsert ? _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].AddNote : _modules_constants__WEBPACK_IMPORTED_MODULE_25__["resource"].UpdateNote;
      var buttonTitle = isInsert ? _modules_globalResource__WEBPACK_IMPORTED_MODULE_26__["globalResource"].Add : _modules_globalResource__WEBPACK_IMPORTED_MODULE_26__["globalResource"].Update;

      var validationResult = this._validateNoteContent();

      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Modal"], {
        isOpen: isShowPopup,
        toggle: this.handleClosePopup,
        backdrop: "static"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["ModalHeader"], {
        toggle: this.handleClosePopup
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement("span", {
        className: "iconpopup"
      }), popupTitle), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["ModalBody"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement("div", {
        className: "modal-body-wrapper"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement("div", {
        className: "note-form-container"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message
        }
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Form"], {
        tag: "form",
        className: "mb-5",
        autocomplete: "off"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Label"], {
        for: "noteContent",
        sm: 3,
        className: "d-flex justify-content-center align-items-center required"
      }, "Content"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Col"], {
        sm: 9
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Input"], {
        type: "textarea",
        className: "input-required",
        name: "noteContent",
        id: "noteContent",
        rows: "8",
        value: Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_24__["htmlDecode"])(this.state.noteContent),
        onChange: this.handleChange,
        invalid: isClickedSubmit && !validationResult.isValid
      }), validationResult.message && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["FormFeedback"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: validationResult.message
        }
      }))))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Button"], {
        type: "button",
        color: "success",
        className: "mr-3",
        disabled: isClickedSubmit ? !validationResult.isValid : false,
        onClick: function onClick() {
          return _this3.handleSave(validationResult.isValid);
        }
      }, buttonTitle), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_19___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_22__["Button"], {
        type: "button",
        color: "info",
        onClick: this.handleClosePopup
      }, _modules_globalResource__WEBPACK_IMPORTED_MODULE_26__["globalResource"].Cancel)))));
    }
  }]);

  return InsertUpdateNoteForm;
}(react__WEBPACK_IMPORTED_MODULE_19__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      note = _ref.note;
  var pagingOption = note.pagingOption,
      noteInfo = note.noteInfo,
      isShowPopup = note.isShowPopup;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default()({}, {
    affId: affId,
    isShowPopup: isShowPopup,
    pagingOption: pagingOption,
    noteInfo: noteInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_21__["bindActionCreators"])({
    setShowPopup: _actions__WEBPACK_IMPORTED_MODULE_23__["setShowPopup"],
    getNoteList: _actions__WEBPACK_IMPORTED_MODULE_23__["getNoteList"],
    insertNote: _actions__WEBPACK_IMPORTED_MODULE_23__["insertNote"],
    updateNote: _actions__WEBPACK_IMPORTED_MODULE_23__["updateNote"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_20__["connect"])(mapStateToProps, mapDispatchToProps)(InsertUpdateNoteForm));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/note/modules/constants.js":
/*!*******************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/note/modules/constants.js ***!
  \*******************************************************************************/
/*! exports provided: globalResource, maxWidth, maxLength, headerText, resource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "maxLength", function() { return maxLength; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "headerText", function() { return headerText; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "maxWidth", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_0__["maxWidth"]; });

/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"]; });




var maxLength = 1000;
var headerText = {
  Index: _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"].Index,
  Content: 'Content',
  CreatedBy: 'Created By',
  LastModify: 'Last Modify'
};
var resource = {
  RequiredErrorMessage: 'This field is required',
  MaxLengthErrorMessage: 'This field must not exceed 1000 characters',
  AddNote: 'Add Note',
  AddNoteSuccess: 'Add Note is successful',
  UpdateNote: 'Update Note',
  UpdateNoteSuccess: 'Update Note is successful',
  Failed: 'The request is failed'
};

/***/ }),

/***/ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js":
/*!**************************************************************************!*\
  !*** ./node_modules/@9softstudio/react-fixed-table-header/dist/index.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

!function(e,t){ true?module.exports=t(__webpack_require__(/*! react */ "./node_modules/react/index.js")):undefined}(window,function(e){return function(e){var t={};function n(r){if(t[r])return t[r].exports;var o=t[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}return n.m=e,n.c=t,n.d=function(e,t,r){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:r})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)n.d(r,o,function(t){return e[t]}.bind(null,o));return r},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="/dist/",n(n.s=4)}([function(t,n){t.exports=e},function(e,t,n){e.exports=n(7)()},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});t.SCROLLBAR_WIDTH=19,t.GUTTER=15,t.DEFAULT_MILLISECOND_FOR_WAITING=500,t.DEFAULT_COLUMN_WIDTH=100},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=u(o),i=u(n(1));function u(e){return e&&e.__esModule?e:{default:e}}var l=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.onKeyUpHandle=function(e,t){if(13===(e.keyCode?e.keyCode:e.which)){var r=parseInt(n.state.inputValue);r>0&&r<=t&&n.goToPage(r,n.props.pageOption.PageSize)}},n.onInputChangeHandler=function(e){var t=e.target.value;/^\d*$/.test(t)&&n._updateState(t)},n.state={inputValue:n.props.pageOption.PageIndex},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"componentDidUpdate",value:function(e,t){var n=this.props.pageOption.PageIndex;n!==e.pageOption.PageIndex&&this._updateState(n)}},{key:"goToPage",value:function(e,t){this.props.onPaging&&this.props.onPaging(e,t)}},{key:"onClickHandle",value:function(e,t){e||t()}},{key:"_updateState",value:function(e){this.setState({inputValue:e})}},{key:"_renderPageSizeSelection",value:function(e,t,n){var r=this;return a.default.createElement("select",{className:"pagesize-select",disabled:e,value:n,onChange:function(e){return r.goToPage(1,Number(e.target.value))}},this._renderPageSizeOption(this.props.pageOption.PageList))}},{key:"_renderPageSizeOption",value:function(e){return e.map(function(e,t){return a.default.createElement("option",{key:"pagesize"+t,value:e},e)})}},{key:"_renderPageIcon",value:function(e,t,n){var r=this;return a.default.createElement("a",{href:"javascript:;",className:"icon "+e+" "+(t?"disable":""),onClick:function(){return r.onClickHandle(t,n)}})}},{key:"_renderPageInput",value:function(e,t){var n=this;return a.default.createElement("div",{className:"page-input"},a.default.createElement("span",null,"Page "),a.default.createElement("input",{type:"text",className:"page-form",value:e,maxLength:"4",size:"2",onChange:this.onInputChangeHandler,onKeyUp:function(e){return n.onKeyUpHandle(e,t)}}),a.default.createElement("span",null," of "+t," "))}},{key:"_renderPageLabel",value:function(e,t){return a.default.createElement("div",{className:"page-input"},a.default.createElement("span",null,"Page ",e+" of "+t+" "))}},{key:"_renderPageInfo",value:function(e,t,n){var r=(e-1)*t+1,o=e*t;return a.default.createElement("span",{className:"page-info"},"View "+r+"-"+(o>n?n:o)+" of "+n)}},{key:"render",value:function(){var e=this,t=this.props,n=t.isShowPagingInfo,r=t.isAllowInputPageIndex,o=t.pageOption,i=o.PageIndex,u=o.PageSize,l=o.TotalItem,c=o.PageList,s=this.state.inputValue,f=isNaN(parseInt(s)),p=l>c[0],d=Math.ceil(l/u);return a.default.createElement("div",{className:"page-container center-align",style:{display:p?"block":"none"}},this._renderPageIcon("page-first",1===i,function(){return e.goToPage(1,u)}),this._renderPageIcon("page-prev",f||i<=1||i>d,function(){return e.goToPage(i-1,u)}),r?this._renderPageInput(s,d):this._renderPageLabel(i,d),this._renderPageIcon("page-next",f||i>=d||i<1,function(){return e.goToPage(i+1,u)}),this._renderPageIcon("page-last",i===d,function(){return e.goToPage(d,u)}),this._renderPageSizeSelection(f,i,u),n&&this._renderPageInfo(i,u,l))}}]),t}();l.propTypes={pageOption:i.default.shape({PageIndex:i.default.number,PageSize:i.default.number,PageList:i.default.arrayOf(i.default.number),TotalItem:i.default.number}),onPaging:i.default.func,isShowPagingInfo:i.default.bool,isAllowInputPageIndex:i.default.bool},l.defaultProps={pageOption:{PageIndex:1,PageSize:50,PageList:[50,100,200],TotalItem:0},isShowPagingInfo:!0,isAllowInputPageIndex:!0},t.default=l},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.Pager=t.Cell=t.Row=t.Table=void 0;var r=u(n(5)),o=u(n(10)),a=u(n(11)),i=u(n(3));function u(e){return e&&e.__esModule?e:{default:e}}t.Table=r.default,t.Row=o.default,t.Cell=a.default,t.Pager=i.default},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=p(a),u=p(n(6)),l=p(n(1)),c=p(n(9)),s=n(2),f=p(n(3));function p(e){return e&&e.__esModule?e:{default:e}}var d=document.body.clientWidth;var h=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));y.call(n),n.bodyWrapper=null,n.footerWrapper=null;n.Header=(0,c.default)({className:"header-content",isHeader:!0});var r={className:"body-content",getRef:function(e){return n.bodyWrapper=e}};n.Body=(0,c.default)(r);var o={className:"footer-content",getRef:function(e){return n.footerWrapper=e}};n.Footer=(0,c.default)(o);var a=n.props,i=a.maxWidth,u=a.bodyHeight,l=a.adjustedHeight,f=i||d;return n.diffWidth=d-f,n.adjustedHeight=l,n.debounceResizing=function(e,t){var n=void 0;return function(){var r=this,o=arguments;clearTimeout(n),n=setTimeout(function(){e.apply(r,o)},t||s.DEFAULT_MILLISECOND_FOR_WAITING)}}(n._handleResize),n.state={maxWidth:f,contentHeight:u,columnsWidth:n._getColumnsWidth(e.header)},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,a.Component),o(t,[{key:"_getColumnsWidth",value:function(e){var t=this,n=Array.isArray(e);if(n&&0===e.length)return null;var r=[],o={};return function n(a,i){for(var u=0;u<a.length;u++){var l=a[u];if(l){var c=l.props,f=Number(c.colSpan);if(f&&f>1){var p=i+1;void 0===o[p]&&(o[p]=0);var d=o[p],h=d+f-1,y=e[p].props.children,b=t._getNextColspanCells(y,d,h);o[p]+=b.length,n(b,p)}else{var g=c.colWidth?c.colWidth:s.DEFAULT_COLUMN_WIDTH;r.push(g)}}}}(n?e[0].props.children:e.props.children,0),r}},{key:"_getNextColspanCells",value:function(e,t,n){for(var r=[],o=t;o<=n;o++){var a=e[o],i=o<e.length;a||!i?(a.props.colSpan&&(n-=1),r.push(a)):n+=1}return r}},{key:"componentDidMount",value:function(){this._handleResize(),window.addEventListener("resize",this.debounceResizing)}},{key:"componentWillUnmount",value:function(){window.removeEventListener("resize",this.debounceResizing)}},{key:"componentDidUpdate",value:function(e,t){this.props.header!=e.header&&this.setState({columnsWidth:this._getColumnsWidth(this.props.header)}),this.props.shouldResetScrollPosition&&e.body!=this.props.body&&function(e,t){var n=-e.scrollTop/(t/15);requestAnimationFrame(function(){var t=null;t=setInterval(function(){0!=e.scrollTop?e.scrollTop+=n:clearInterval(t)},15)})}(this.bodyWrapper,200)}},{key:"_calculateBodyHeight",value:function(){var e=window.innerHeight,t=this.footerWrapper?this.footerWrapper.offsetHeight:0;return e-(this.bodyWrapper.offsetTop+t+70)-this.adjustedHeight}},{key:"_getUpdatedColumnLayout",value:function(){var e=this,t=this.props,n=t.width,r=t.autoWidth,o=t.containerPadding,a=r?this.state.maxWidth:n;return a=a-o-s.SCROLLBAR_WIDTH,this.columnWidthSum&&this.state.columnsWidth.map(function(t){return a/e.columnWidthSum*t})}},{key:"render",value:function(){var e=this.props,t=e.width,n=e.autoWidth,o=e.minWidth,a=e.header,l=e.body,c=e.footer,s=e.isPaging,p=e.pageOption,d=e.onPaging,h=e.containerPadding,y=this.state.maxWidth-h,b=t-h,g=this.columnWidthSum&&this.columnWidthSum!==b?this._getUpdatedColumnLayout():this.state.columnsWidth;if(!g)return null;var m={width:b,autoWidth:n,minWidth:o,maxWidth:y},v=i.default.createElement(u.default,{columnLayout:g}),_=this.Header,O=this.Body,P=this.Footer;return i.default.createElement("div",null,i.default.createElement("div",{className:"table-container",style:{maxWidth:y}},i.default.createElement(_,m,v,a),l&&i.default.createElement(O,r({},m,{maxHeight:this.state.contentHeight}),v,l),c&&i.default.createElement(P,m,v,c)),s&&p&&i.default.createElement(f.default,{pageOption:p,onPaging:d}))}},{key:"columnWidthSum",get:function(){var e=this.state.columnsWidth;return e&&e.length&&!e.find(function(e){return"number"!=typeof e})?e.reduce(function(e,t){return e+t},0):null}}]),t}();h.propTypes={width:l.default.oneOfType([l.default.number,l.default.string]),maxWidth:l.default.number,minWidth:l.default.number,autoWidth:l.default.bool,bodyHeight:l.default.number,autoHeight:l.default.bool,header:l.default.oneOfType([l.default.object,l.default.arrayOf(l.default.object)]).isRequired,body:l.default.arrayOf(l.default.object),footer:l.default.arrayOf(l.default.object),isPaging:l.default.bool,onPaging:l.default.func,pageOption:l.default.object,adjustedHeight:l.default.number,containerPadding:l.default.number,shouldResetScrollPosition:l.default.bool},h.defaultProps={width:d,maxWidth:d,autoWidth:!0,isPaging:!1,adjustedHeight:0,bodyHeight:0,autoHeight:!0,containerPadding:30,shouldResetScrollPosition:!0};var y=function(){var e=this;this._handleResize=function(t){if(t&&t.preventDefault(),e.bodyWrapper&&e.props.autoHeight){var n=e._calculateBodyHeight();e.state.contentHeight!==n&&e.setState({contentHeight:n})}var r=document.body.clientWidth-e.diffWidth;e.state.maxWidth!==r&&e.setState({maxWidth:r})}};t.default=h},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=u(o),i=u(n(1));function u(e){return e&&e.__esModule?e:{default:e}}var l=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"_getRowLayout",value:function(){return this.props.columnLayout.map(function(e,t){var n=isNaN(e)?e:e+"px";return a.default.createElement("td",{key:"cellLayout"+t,style:{width:n}})})}},{key:"render",value:function(){return a.default.createElement("tr",{className:"first-row"},this._getRowLayout())}}]),t}();l.propTypes={columnLayout:i.default.arrayOf(i.default.oneOfType([i.default.number,i.default.string])).isRequired},t.default=l},function(e,t,n){"use strict";var r=n(8);function o(){}e.exports=function(){function e(e,t,n,o,a,i){if(i!==r){var u=new Error("Calling PropTypes validators directly is not supported by the `prop-types` package. Use PropTypes.checkPropTypes() to call them. Read more at http://fb.me/use-check-prop-types");throw u.name="Invariant Violation",u}}function t(){return e}e.isRequired=e;var n={array:e,bool:e,func:e,number:e,object:e,string:e,symbol:e,any:e,arrayOf:t,element:e,instanceOf:t,node:e,objectOf:t,oneOf:t,oneOfType:t,shape:t,exact:t};return n.checkPropTypes=o,n.PropTypes=n,n}},function(e,t,n){"use strict";e.exports="SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED"},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=c(a),u=c(n(1)),l=n(2);function c(e){return e&&e.__esModule?e:{default:e}}function s(e,t){var n={};for(var r in e)t.indexOf(r)>=0||Object.prototype.hasOwnProperty.call(e,r)&&(n[r]=e[r]);return n}t.default=function(e,t){var n,c;return c=n=function(n){function u(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,u),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(u.__proto__||Object.getPrototypeOf(u)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(u,a.Component),o(u,[{key:"render",value:function(){var n=this.props,o=n.tableClass,a=(n.width,n.autoWidth,n.maxWidth,n.minWidth,n.maxHeight,s(n,["tableClass","width","autoWidth","maxWidth","minWidth","maxHeight"])),u=e.isHeader,l=e.getRef,c=s(e,["isHeader","getRef"]);return i.default.createElement("div",r({},a,{style:this.containerStyle,ref:l},c),i.default.createElement("div",{className:"table-wrapper"},i.default.createElement("table",r({style:this.tableStyle,className:o},t),u?i.default.createElement("thead",null,this.props.children):i.default.createElement("tbody",null,this.props.children))))}},{key:"containerWidth",get:function(){var e=this.props,t=e.autoWidth,n=e.maxWidth,r=e.width;return t?n:r}},{key:"containerStyle",get:function(){var e=this.props,t=e.minWidth,n=e.maxHeight;return r({},this.props.style,{width:this.containerWidth,minWidth:t,maxHeight:n})}},{key:"tableStyle",get:function(){var e=this.props.minWidth;return{width:(e?Math.max(this.containerWidth,e):this.containerWidth)-l.SCROLLBAR_WIDTH}}}]),u}(),n.propTypes={tableClass:u.default.string,width:u.default.oneOfType([u.default.number,u.default.string]).isRequired,maxWidth:u.default.number.isRequired,minWidth:u.default.number,autoWidth:u.default.bool,maxHeight:u.default.number},n.defaultProps={tableClass:"table",autoWidth:!0},c}},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=function(e){return e&&e.__esModule?e:{default:e}}(o);var i=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"render",value:function(){return a.default.createElement("tr",this.props,this.props.children)}}]),t}();t.default=i},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=l(a),u=l(n(1));function l(e){return e&&e.__esModule?e:{default:e}}var c=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,a.Component),o(t,[{key:"render",value:function(){var e=this.props,t=e.header,n=(e.colWidth,e.sortable),o=e.sorting,a=e.asc,u=e.onClick,l=function(e,t){var n={};for(var r in e)t.indexOf(r)>=0||Object.prototype.hasOwnProperty.call(e,r)&&(n[r]=e[r]);return n}(e,["header","colWidth","sortable","sorting","asc","onClick"]);return t?n?i.default.createElement("th",r({},l,{className:"sortable "+(o?"sorting":""),onClick:u}),this.props.children,i.default.createElement("span",{className:o?a?"asc":"desc":""})):i.default.createElement("th",l,this.props.children):i.default.createElement("td",l,this.props.children)}}]),t}();c.propTypes={header:u.default.bool,sortable:u.default.bool,sorting:u.default.bool,asc:u.default.bool,onClick:u.default.func},c.defaultProps={header:!1,sortable:!1,sorting:!1,asc:!0,onClick:function(){}},t.default=c}])});

/***/ })

}]);
//# sourceMappingURL=note.chunk.js.map