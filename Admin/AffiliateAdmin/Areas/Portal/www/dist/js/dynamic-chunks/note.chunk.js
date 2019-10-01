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
  return (
    /*#__PURE__*/
    function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(dispatch) {
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
    }()
  );
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
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2() {
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
    }))
  );
};
var insertNote = function insertNote(param) {
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee3() {
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
    }))
  );
};
var updateNote = function updateNote(param) {
  return (
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee4() {
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
    }))
  );
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
  return react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_2__["default"], null), react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_InsertUpdateNoteForm__WEBPACK_IMPORTED_MODULE_3__["default"], null));
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

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
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/note/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/note/actions/index.js");


















var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(Content).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleShowPopup", function (noteId) {
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

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handlePaging", function (pageIndex, pageSize) {
      var newParam = {
        affId: _this.props.affId,
        pageIndex: pageIndex,
        pageSize: pageSize
      };

      _this.props.getNoteList(newParam);
    });

    var pagingOption = props.pagingOption,
        affId = props.affId;

    var param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
      affId: affId,
      pageIndex: 1,
      pageSize: pagingOption.PageSize
    });

    _this.props.getNoteList(param);

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default()(Content, [{
    key: "_renderHeader",
    value: function _renderHeader() {
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Row"], {
        key: "thead"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 35,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Index
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Index), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 580,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Content
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Content), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 150,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].CreatedBy
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].CreatedBy), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 200,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].LastModify
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].LastModify), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 35,
        title: _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].Edit
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].Edit));
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
        dataRows.push(react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Row"], null, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
          className: "text-center",
          colSpan: "5"
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index) {
      var _this2 = this;

      var noteContent = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_15__["htmlDecode"])(dataItem.NoteContent);
      var createdBy = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_15__["htmlDecode"])(dataItem.CreatedBy);
      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Row"], {
        key: "dataRow-".concat(index)
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: index,
        className: "text-center"
      }, index), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: noteContent
      }, noteContent), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: createdBy
      }, createdBy), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.LastModifyText,
        className: "text-center"
      }, dataItem.LastModifyText), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: "Edit Note",
        className: "text-center"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        className: "p-0",
        color: "link",
        onClick: function onClick() {
          return _this2.handleShowPopup(dataItem.NoteId);
        }
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("span", {
        className: "oi oi-pencil text-secondary"
      }))));
    }
  }, {
    key: "getNoteContentById",
    value: function () {
      var _getNoteContentById = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(noteId) {
        var affId, data, NoteId, NoteContent, noteItem;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
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
      return noteList && react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("div", {
        className: "data-content"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("div", {
        className: "my-2"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        outline: true,
        type: "button",
        onClick: function onClick() {
          return _this3.handleShowPopup(0);
        }
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].Add)), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("div", {
        className: "table-responsive table-striped"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Table"], {
        maxWidth: _modules_constants__WEBPACK_IMPORTED_MODULE_16__["maxWidth"],
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
}(react__WEBPACK_IMPORTED_MODULE_10__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      note = _ref.note;
  var pagingOption = note.pagingOption,
      noteList = note.noteList,
      noteInfo = note.noteInfo;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
    affId: affId,
    pagingOption: pagingOption,
    noteList: noteList,
    noteInfo: noteInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_12__["bindActionCreators"])({
    setShowPopup: _actions__WEBPACK_IMPORTED_MODULE_17__["setShowPopup"],
    setNoteInfo: _actions__WEBPACK_IMPORTED_MODULE_17__["setNoteInfo"],
    toggleLoaderUserPanel: _actions__WEBPACK_IMPORTED_MODULE_17__["toggleLoaderUserPanel"],
    getNoteList: _actions__WEBPACK_IMPORTED_MODULE_17__["getNoteList"],
    getNoteContentById: _actions__WEBPACK_IMPORTED_MODULE_17__["getNoteContentById"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_11__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

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
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/note/actions/index.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/note/modules/constants.js");
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");



















var InsertUpdateNoteForm =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(InsertUpdateNoteForm, _PureComponent);

  function InsertUpdateNoteForm(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, InsertUpdateNoteForm);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(InsertUpdateNoteForm).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChange", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, event.target.name, event.target.value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "_validateNoteContent", function () {
      var noteContent = _this.state.noteContent;
      var result = {
        isValid: !!noteContent && noteContent.length <= _modules_constants__WEBPACK_IMPORTED_MODULE_17__["maxLength"],
        message: ''
      };

      if (!result.isValid) {
        result.message = noteContent ? _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].MaxLengthErrorMessage : _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].RequiredErrorMessage;
      }

      return result;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleSave", function (isValid) {
      _this.setState({
        isClickedSubmit: true
      });

      if (isValid) {
        var newNoteInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, _this.props.noteInfo, {
          noteContent: _this.state.noteContent
        });

        _this.Save(newNoteInfo);
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "hideMessage", function () {
      _this.setState({
        isShowMessage: false
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleClosePopup", function () {
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

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(InsertUpdateNoteForm, [{
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
      var _Save = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(noteInfo) {
        var _this2 = this;

        var isInsert, message, affId, param, result, pagingOption, _param;

        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                isInsert = noteInfo.noteId === 0;
                message = isInsert ? _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].AddNoteSuccess : _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].UpdateNoteSuccess;
                affId = this.props.affId;
                param = _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_1___default()({}, noteInfo, {
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
                    _param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
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
                    message: _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].Failed
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
      var popupTitle = isInsert ? _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].AddNote : _modules_constants__WEBPACK_IMPORTED_MODULE_17__["resource"].UpdateNote;
      var buttonTitle = isInsert ? _modules_globalResource__WEBPACK_IMPORTED_MODULE_18__["globalResource"].Add : _modules_globalResource__WEBPACK_IMPORTED_MODULE_18__["globalResource"].Update;

      var validationResult = this._validateNoteContent();

      return react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Modal"], {
        isOpen: isShowPopup,
        toggle: this.handleClosePopup,
        backdrop: "static"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["ModalHeader"], {
        toggle: this.handleClosePopup
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "iconpopup"
      }), popupTitle), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["ModalBody"], null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "modal-body-wrapper"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "note-form-container"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message
        }
      })), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Form"], {
        tag: "form",
        className: "mb-5",
        autocomplete: "off"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "noteContent",
        sm: 3,
        className: "d-flex justify-content-center align-items-center required"
      }, "Content"), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Col"], {
        sm: 9
      }, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "textarea",
        className: "input-required",
        name: "noteContent",
        id: "noteContent",
        rows: "8",
        value: Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_16__["htmlDecode"])(this.state.noteContent),
        onChange: this.handleChange,
        invalid: isClickedSubmit && !validationResult.isValid
      }), validationResult.message && react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormFeedback"], null, react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: validationResult.message
        }
      }))))), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        type: "button",
        color: "success",
        className: "mr-3",
        disabled: isClickedSubmit ? !validationResult.isValid : false,
        onClick: function onClick() {
          return _this3.handleSave(validationResult.isValid);
        }
      }, buttonTitle), react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        type: "button",
        color: "info",
        onClick: this.handleClosePopup
      }, _modules_globalResource__WEBPACK_IMPORTED_MODULE_18__["globalResource"].Cancel)))));
    }
  }]);

  return InsertUpdateNoteForm;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref) {
  var affCommonInfo = _ref.affCommonInfo,
      note = _ref.note;
  var pagingOption = note.pagingOption,
      noteInfo = note.noteInfo,
      isShowPopup = note.isShowPopup;
  var affId = affCommonInfo.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
    affId: affId,
    isShowPopup: isShowPopup,
    pagingOption: pagingOption,
    noteInfo: noteInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setShowPopup: _actions__WEBPACK_IMPORTED_MODULE_15__["setShowPopup"],
    getNoteList: _actions__WEBPACK_IMPORTED_MODULE_15__["getNoteList"],
    insertNote: _actions__WEBPACK_IMPORTED_MODULE_15__["insertNote"],
    updateNote: _actions__WEBPACK_IMPORTED_MODULE_15__["updateNote"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(InsertUpdateNoteForm));

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
  Index: '#',
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

/***/ })

}]);
//# sourceMappingURL=note.chunk.js.map