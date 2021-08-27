(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[14],{

/***/ "./Areas/Portal/www/source/js/modules/constants.js":
/*!*********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/constants.js ***!
  \*********************************************************/
/*! exports provided: defaultPagingOption, minMonthDate, shortedMonthNames, monthNames, money, selectedAllProduct, maxWidth, spinnerLoaderWidth, spinnerLoaderUserPanelHeight, minDate, sixMonth, submitFail, pcLaptop, mobile, deviceTypes, regExp, changePasswordMessages, specialCharacters, defaultDatetimeFormat */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "defaultPagingOption", function() { return defaultPagingOption; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "minMonthDate", function() { return minMonthDate; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "shortedMonthNames", function() { return shortedMonthNames; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "monthNames", function() { return monthNames; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "money", function() { return money; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "selectedAllProduct", function() { return selectedAllProduct; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "maxWidth", function() { return maxWidth; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "spinnerLoaderWidth", function() { return spinnerLoaderWidth; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "spinnerLoaderUserPanelHeight", function() { return spinnerLoaderUserPanelHeight; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "minDate", function() { return minDate; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "sixMonth", function() { return sixMonth; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "submitFail", function() { return submitFail; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "pcLaptop", function() { return pcLaptop; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "mobile", function() { return mobile; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deviceTypes", function() { return deviceTypes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "regExp", function() { return regExp; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "changePasswordMessages", function() { return changePasswordMessages; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "specialCharacters", function() { return specialCharacters; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "defaultDatetimeFormat", function() { return defaultDatetimeFormat; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);


var _deviceTypes;

var defaultPagingOption = {
  PageIndex: 1,
  PageSize: 20,
  TotalItem: 0,
  PageList: [10, 20, 50, 100, 200, 500]
};
var minMonthDate = {
  month: 12,
  year: 2009
};
var shortedMonthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var money = {
  MaxValue: 922337203685477,
  MinValue: -922337203685477
};
var selectedAllProduct = 0;
var maxWidth = window.innerWidth - 282;
var maxUserPanelHeight = window.innerHeight - 234;
var spinnerLoaderWidth = maxWidth / 2 - 32;
var spinnerLoaderUserPanelHeight = maxUserPanelHeight / 2 - 32;
var minDate = new Date(2009, 11, 1);
var sixMonth = 6;
var submitFail = {
  IsSuccessful: false,
  Message: 'Update Failed!'
};
var pcLaptop = 'PC / Laptop';
var mobile = 'Mobile';
var deviceTypes = (_deviceTypes = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_deviceTypes, pcLaptop, 0), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_deviceTypes, mobile, 1), _deviceTypes);
var regExp = {
  CheckPassword: "^(?=.*[0-9]+.*[0-9]+)(?=.*[a-zA-Z].*[a-zA-Z]).{6,}$"
};
var changePasswordMessages = {
  RequiredConfirmPassword: "Confirm Password is required!",
  ChangePasswordSuccess: "Change password is successful!",
  ConfirmPasswordIncorrect: 'Confirm Password is incorrect!',
  ChangePasswordFailed: 'Change password is failed!'
};
var specialCharacters = ["#", "<", "$", "+", "%", ">", "!", "`", "&", "*", "'", "|", "{", "}", "?", "\"", "=", "/", ":", "\\", "@"];
var defaultDatetimeFormat = "dd/MM/yyyy HH:mm:ss";

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/htmlHelper.js":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/htmlHelper.js ***!
  \**********************************************************/
/*! exports provided: htmlDecode, trimText */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "htmlDecode", function() { return htmlDecode; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "trimText", function() { return trimText; });
var htmlDecode = function htmlDecode(text) {
  var span = document.createElement("span");
  span.innerHTML = text;
  return span.textContent;
};
var trimText = function trimText(text) {
  if (typeof text === "undefined" || text === null) return "";else return text;
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/index.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/index.js ***!
  \****************************************************************************************/
/*! exports provided: toggleLoader, setPagingOption, setReportTitle, setParam, setSelectedRequestId, setSelectedRequest, setShouldResetScrollPosition, setGridData, setIsClickSearch, setProcRequestSummary, setOriginalStatus, fetchData, getProcessingRequestDetail, insertProcessingRequest, updateProcessingRequest, deleteProcessingRequest */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setReportTitle", function() { return setReportTitle; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setParam", function() { return setParam; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setSelectedRequestId", function() { return setSelectedRequestId; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setSelectedRequest", function() { return setSelectedRequest; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setShouldResetScrollPosition", function() { return setShouldResetScrollPosition; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setGridData", function() { return setGridData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setIsClickSearch", function() { return setIsClickSearch; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setProcRequestSummary", function() { return setProcRequestSummary; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setOriginalStatus", function() { return setOriginalStatus; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "fetchData", function() { return fetchData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getProcessingRequestDetail", function() { return getProcessingRequestDetail; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "insertProcessingRequest", function() { return insertProcessingRequest; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "updateProcessingRequest", function() { return updateProcessingRequest; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deleteProcessingRequest", function() { return deleteProcessingRequest; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _types__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./types */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/types.js");
/* harmony import */ var _actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../../actions/loaderAction */ "./Areas/Portal/www/source/js/actions/loaderAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "toggleLoader", function() { return _actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"]; });

/* harmony import */ var _actions_pagingAction__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../actions/pagingAction */ "./Areas/Portal/www/source/js/actions/pagingAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "setPagingOption", function() { return _actions_pagingAction__WEBPACK_IMPORTED_MODULE_5__["setPagingOption"]; });








var setReportTitle = function setReportTitle(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_REPORT_TITLE"],
    data: data
  };
};
var setParam = function setParam(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_PARAM"],
    data: data
  };
};
var setSelectedRequestId = function setSelectedRequestId(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_SELECTED_REQUEST_ID"],
    data: data
  };
};
var setSelectedRequest = function setSelectedRequest(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_SELECTED_PROC_REQUEST"],
    data: data
  };
};
var setShouldResetScrollPosition = function setShouldResetScrollPosition(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_SHOULD_RESET_SCROLL_POSITION"],
    data: data
  };
};
var setGridData = function setGridData(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_GRID_DATA"],
    data: data
  };
};
var setIsClickSearch = function setIsClickSearch(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_IS_CLICK_SEARCH"],
    data: data
  };
};
var setProcRequestSummary = function setProcRequestSummary(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_PROC_REQUEST_SUMMARY"],
    data: data
  };
};
var setOriginalStatus = function setOriginalStatus(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_3__["SET_ORGINAL_STATUS"],
    data: data
  };
};
var fetchData = function fetchData(param) {
  return /*#__PURE__*/function () {
    var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(dispatch) {
      var url, response, data;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(true));
              url = 'Portal/ProcessingRequest/GetProcessingRequestList';
              _context.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 4:
              response = _context.sent;
              data = response && response.status === 200 ? response.data : null;
              dispatch(setGridData(data ? data.MainData : []));
              dispatch(Object(_actions_pagingAction__WEBPACK_IMPORTED_MODULE_5__["setPagingOption"])({
                TotalItem: data ? data.TotalItem : 0,
                PageIndex: param.PageIndex,
                PageSize: param.PageSize
              }));
              dispatch(setProcRequestSummary(data ? data.Summary : null));
              dispatch(setShouldResetScrollPosition(true));
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(false));

            case 11:
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
var getProcessingRequestDetail = function getProcessingRequestDetail(param) {
  return /*#__PURE__*/function () {
    var _ref2 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2(dispatch) {
      var url, response, data;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee2$(_context2) {
        while (1) {
          switch (_context2.prev = _context2.next) {
            case 0:
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(true));
              url = 'Portal/ProcessingRequest/GetProcessingRequestDetail';
              _context2.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 4:
              response = _context2.sent;
              data = response && response.status === 200 ? response.data : null;
              dispatch(setSelectedRequest(data));
              dispatch(setOriginalStatus(data.Status));
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(false));
              return _context2.abrupt("return", data);

            case 10:
            case "end":
              return _context2.stop();
          }
        }
      }, _callee2);
    }));

    return function (_x2) {
      return _ref2.apply(this, arguments);
    };
  }();
};
var insertProcessingRequest = function insertProcessingRequest(param) {
  return /*#__PURE__*/function () {
    var _ref3 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee3(dispatch) {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee3$(_context3) {
        while (1) {
          switch (_context3.prev = _context3.next) {
            case 0:
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(true));
              url = 'Portal/ProcessingRequest/InsertProcessingRequest';
              _context3.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 4:
              response = _context3.sent;
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(false));
              return _context3.abrupt("return", response && response.status === 200 ? response.data : null);

            case 7:
            case "end":
              return _context3.stop();
          }
        }
      }, _callee3);
    }));

    return function (_x3) {
      return _ref3.apply(this, arguments);
    };
  }();
};
var updateProcessingRequest = function updateProcessingRequest(param) {
  return /*#__PURE__*/function () {
    var _ref4 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee4(dispatch) {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee4$(_context4) {
        while (1) {
          switch (_context4.prev = _context4.next) {
            case 0:
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(true));
              url = 'Portal/ProcessingRequest/UpdateProcessingRequest';
              _context4.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, param);

            case 4:
              response = _context4.sent;
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(false));
              return _context4.abrupt("return", response && response.status === 200 ? response.data : null);

            case 7:
            case "end":
              return _context4.stop();
          }
        }
      }, _callee4);
    }));

    return function (_x4) {
      return _ref4.apply(this, arguments);
    };
  }();
};
var deleteProcessingRequest = function deleteProcessingRequest(selectedRequestId) {
  return /*#__PURE__*/function () {
    var _ref5 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee5(dispatch) {
      var url, response;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee5$(_context5) {
        while (1) {
          switch (_context5.prev = _context5.next) {
            case 0:
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(true));
              url = 'Portal/ProcessingRequest/DeleteProcessingRequest';
              _context5.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, {
                RequestId: selectedRequestId
              });

            case 4:
              response = _context5.sent;
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(false));
              return _context5.abrupt("return", response && response.status === 200 ? response.data : null);

            case 7:
            case "end":
              return _context5.stop();
          }
        }
      }, _callee5);
    }));

    return function (_x5) {
      return _ref5.apply(this, arguments);
    };
  }();
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/types.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/types.js ***!
  \****************************************************************************************/
/*! exports provided: SET_REPORT_TITLE, SET_PARAM, SET_GRID_DATA, SET_SELECTED_REQUEST_ID, SET_SHOULD_RESET_SCROLL_POSITION, SET_IS_CLICK_SEARCH, SET_PROC_REQUEST_SUMMARY, SET_SELECTED_PROC_REQUEST, SET_LANGUAGE_LIST, SET_ORGINAL_STATUS */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_REPORT_TITLE", function() { return SET_REPORT_TITLE; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_PARAM", function() { return SET_PARAM; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_GRID_DATA", function() { return SET_GRID_DATA; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_SELECTED_REQUEST_ID", function() { return SET_SELECTED_REQUEST_ID; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_SHOULD_RESET_SCROLL_POSITION", function() { return SET_SHOULD_RESET_SCROLL_POSITION; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_IS_CLICK_SEARCH", function() { return SET_IS_CLICK_SEARCH; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_PROC_REQUEST_SUMMARY", function() { return SET_PROC_REQUEST_SUMMARY; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_SELECTED_PROC_REQUEST", function() { return SET_SELECTED_PROC_REQUEST; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_LANGUAGE_LIST", function() { return SET_LANGUAGE_LIST; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_ORGINAL_STATUS", function() { return SET_ORGINAL_STATUS; });
var SET_REPORT_TITLE = "PROCESSING_REQUEST_SET_REPORT_TITLE";
var SET_PARAM = "PROCESSING_REQUEST_SET_PARAM";
var SET_GRID_DATA = 'PROCESSING_REQUEST_SET_GRID_DATA';
var SET_SELECTED_REQUEST_ID = "PROCESSING_REQUEST_SET_SELECTED_REQUEST_ID";
var SET_SHOULD_RESET_SCROLL_POSITION = 'PROCESSING_REQUEST_SET_SHOULD_RESET_SCROLL_POSITION';
var SET_IS_CLICK_SEARCH = 'PROCESSING_REQUEST_SET_IS_CLICK_SEARCH';
var SET_PROC_REQUEST_SUMMARY = 'PROCESSING_REQUEST_SET_SUMMARY';
var SET_SELECTED_PROC_REQUEST = "PROCESSING_REQUEST_SET_SELECTED_PROC_REQUEST";
var SET_LANGUAGE_LIST = 'SET_LANGUAGE_LIST';
var SET_ORGINAL_STATUS = 'SET_ORGINAL_STATUS';

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/AppContainer.jsx":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/AppContainer.jsx ***!
  \***************************************************************************************************/
/*! exports provided: AppContainer, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppContainer", function() { return AppContainer; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Filter__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/Filter.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/Content.jsx");
/* harmony import */ var _components_Loader__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../../../../components/Loader */ "./Areas/Portal/www/source/js/components/Loader.jsx");
/* harmony import */ var _modules_utils__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../../../modules/utils */ "./Areas/Portal/www/source/js/modules/utils.js");








function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }







 // import { setShowPopupPassword } from '../actions'


var AppContainer = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4___default()(AppContainer, _PureComponent);

  var _super = _createSuper(AppContainer);

  function AppContainer(props) {
    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default()(this, AppContainer);

    return _super.call(this, props);
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default()(AppContainer, [{
    key: "render",
    value: function render() {
      var isShowLoader = this.props.isShowLoader;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_10__["default"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_components_Loader__WEBPACK_IMPORTED_MODULE_13__["default"], {
        show: isShowLoader
      }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_Filter__WEBPACK_IMPORTED_MODULE_11__["default"], null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_12__["default"], null));
    }
  }]);

  return AppContainer;
}(react__WEBPACK_IMPORTED_MODULE_7__["PureComponent"]);

function mapStateToProps(_ref) {
  var isShowLoader = _ref.isShowLoader,
      affId = _ref.affId;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    isShowLoader: isShowLoader,
    affId: affId
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_9__["bindActionCreators"])({}, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_8__["connect"])(mapStateToProps, mapDispatchToProps)(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/Content.jsx":
/*!**********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/Content.jsx ***!
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
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3__);
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
/* harmony import */ var _actions_loaderAction__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../../../actions/loaderAction */ "./Areas/Portal/www/source/js/actions/loaderAction.js");
/* harmony import */ var _components_ReportTitle__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../../../components/ReportTitle */ "./Areas/Portal/www/source/js/components/ReportTitle.jsx");
/* harmony import */ var _GridData__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./GridData */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/GridData.jsx");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _libs_reportExcel__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../libs/reportExcel */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/reportExcel.js");
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ../../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");
/* harmony import */ var _EditRequest_EditRequest__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./EditRequest/EditRequest */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/EditRequest/EditRequest.jsx");
/* harmony import */ var _common_components_NotificationPopup__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ../../../common/components/NotificationPopup */ "./Areas/Portal/www/source/js/pages/common/components/NotificationPopup.jsx");
/* harmony import */ var _actions_index__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ../actions/index */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/index.js");












function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }














var Content = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(Content, _PureComponent);

  var _super = _createSuper(Content);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Content);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleAddNewRequest", function () {
      _this.setState({
        Popup: /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_EditRequest_EditRequest__WEBPACK_IMPORTED_MODULE_21__["default"], {
          onClose: _this.handleHide
        }),
        popupTitle: 'Add Processing Request'
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleEditRequest", /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.mark(function _callee() {
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              _context.next = 2;
              return _this.props.getProcessingRequestDetail(_this.props.selectedRequestId);

            case 2:
              _this.setState({
                Popup: /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_EditRequest_EditRequest__WEBPACK_IMPORTED_MODULE_21__["default"], {
                  onClose: _this.handleHide
                }),
                popupTitle: 'Edit Processing Request'
              });

            case 3:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    })));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleViewRequest", /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.mark(function _callee2() {
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.wrap(function _callee2$(_context2) {
        while (1) {
          switch (_context2.prev = _context2.next) {
            case 0:
              _context2.next = 2;
              return _this.props.getProcessingRequestDetail(_this.props.selectedRequestId);

            case 2:
              _this.setState({
                Popup: /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_EditRequest_EditRequest__WEBPACK_IMPORTED_MODULE_21__["default"], {
                  onClose: _this.handleHide,
                  isReadOnly: true
                }),
                popupTitle: 'Processing Request Detail'
              });

            case 3:
            case "end":
              return _context2.stop();
          }
        }
      }, _callee2);
    })));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleDeleteRequest", function () {
      _this.setState({
        confirmMsg: 'Are you sure to delete this request?',
        isConfirmPopup: true
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_handleCloseNotificationPopup", function (confirmResult) {
      _this.setState({
        confirmMsg: ''
      });

      var selectedRequestId = _this.props.selectedRequestId;

      if (confirmResult && selectedRequestId) {
        _this._deleteRequest(selectedRequestId);
      }

      _this.props.setSelectedRequestId(null);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_deleteRequest", /*#__PURE__*/function () {
      var _ref3 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.mark(function _callee3(selectedRequestId) {
        var result;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.wrap(function _callee3$(_context3) {
          while (1) {
            switch (_context3.prev = _context3.next) {
              case 0:
                _context3.next = 2;
                return _this.props.deleteProcessingRequest(selectedRequestId);

              case 2:
                result = _context3.sent;

                if (result) {
                  if (result.IsSuccessful) {
                    _this.setState({
                      confirmMsg: 'Request has been deleted successfully!',
                      isConfirmPopup: false
                    });

                    _this.props.fetchData(_this.props.param);
                  } else {
                    _this.setState({
                      confirmMsg: result.Message,
                      isConfirmPopup: false
                    });
                  }
                } else {
                  _this.setState({
                    confirmMsg: messages.AnError,
                    isConfirmPopup: false
                  });
                }

              case 4:
              case "end":
                return _context3.stop();
            }
          }
        }, _callee3);
      }));

      return function (_x) {
        return _ref3.apply(this, arguments);
      };
    }());

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleHide", function () {
      _this.setState({
        Popup: null
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleExportExcel", /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_3___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.mark(function _callee4() {
      var _this$props, param, reportTitle, url, postData, response, reportExcel;

      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_2___default.a.wrap(function _callee4$(_context4) {
        while (1) {
          switch (_context4.prev = _context4.next) {
            case 0:
              _this.props.toggleLoader(true);

              _this$props = _this.props, param = _this$props.param, reportTitle = _this$props.reportTitle;
              url = "Portal/ProcessingRequest/GetProcessingRequestList";
              postData = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, param, {
                IsExportExcel: true
              });
              _context4.next = 6;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_19__["post"])(url, postData);

            case 6:
              response = _context4.sent;
              reportExcel = new _libs_reportExcel__WEBPACK_IMPORTED_MODULE_18__["default"]({
                MainData: {},
                TotalItem: 0
              }, reportTitle);

              if (response && response.status === 200) {
                reportExcel = new _libs_reportExcel__WEBPACK_IMPORTED_MODULE_18__["default"](response.data, reportTitle);
              }

              reportExcel.export();

              _this.props.toggleLoader(false);

            case 11:
            case "end":
              return _context4.stop();
          }
        }
      }, _callee4);
    })));

    _this.state = {
      Popup: null,
      popupTitle: '',
      confirmMsg: null,
      isConfirmPopup: false
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Content, [{
    key: "_buildReportTitle",
    value: function _buildReportTitle(reportTitle) {
      var isDisplayExportExcelBtn = this.props.isDisplayExportExcelBtn;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "d-flex align-items-center"
      }, reportTitle, isDisplayExportExcelBtn && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("button", {
        type: "button",
        className: "ml-auto btn btn-link btn-excel",
        onClick: this.handleExportExcel,
        title: "Export Excel"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi icon-excel"
      })));
    }
  }, {
    key: "render",
    value: function render() {
      var _this$props2 = this.props,
          reportTitle = _this$props2.reportTitle,
          isModifiable = _this$props2.isModifiable,
          isClickSearch = _this$props2.isClickSearch,
          dataSummary = _this$props2.dataSummary;
      var _this$state = this.state,
          Popup = _this$state.Popup,
          popupTitle = _this$state.popupTitle,
          confirmMsg = _this$state.confirmMsg,
          isConfirmPopup = _this$state.isConfirmPopup;
      var showPopup = Popup !== null;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "data-content my-2"
      }, isModifiable && isClickSearch && dataSummary && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        className: "mt-0",
        sm: 1
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Button"], {
        outline: true,
        type: "button",
        className: "text-secondary",
        onClick: this.handleAddNewRequest
      }, "Add ", /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-plus"
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        sm: 11,
        className: "text-right"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", null, "Status count:"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "pl-3"
      }, dataSummary.Draft + ' Draft'), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "text-primary pl-3"
      }, dataSummary.Submitted + ' Submitted'), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "text-success pl-3"
      }, dataSummary.Approved + ' Approved'), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "text-danger pl-3"
      }, dataSummary.Rejected + ' Rejected'))), reportTitle && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_components_ReportTitle__WEBPACK_IMPORTED_MODULE_15__["default"], {
        content: this._buildReportTitle(reportTitle)
      }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_GridData__WEBPACK_IMPORTED_MODULE_16__["default"], {
        handleView: this.handleViewRequest,
        handleEdit: this.handleEditRequest,
        handleDelete: this.handleDeleteRequest
      }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Modal"], {
        isOpen: showPopup,
        toggle: this.handleHide,
        size: "lg",
        backdrop: "static"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["ModalHeader"], {
        toggle: this.handleHide
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "iconpopup"
      }), popupTitle), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["ModalBody"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "modal-body-wrapper"
      }, Popup))), !!confirmMsg && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_common_components_NotificationPopup__WEBPACK_IMPORTED_MODULE_22__["NotificationPopup"], {
        message: confirmMsg,
        isConfirmPopup: isConfirmPopup,
        onClose: this._handleCloseNotificationPopup
      }));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref5) {
  var reportTitle = _ref5.reportTitle,
      param = _ref5.param,
      claimsPrincipal = _ref5.claimsPrincipal,
      isClickSearch = _ref5.isClickSearch,
      dataSummary = _ref5.dataSummary,
      selectedRequestId = _ref5.selectedRequestId;
  var isModifiable = claimsPrincipal.hasClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_20__["claimResource"].ProcRequestMarketingRole) || claimsPrincipal.hasClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_20__["claimResource"].ProcRequestManagementRole);
  var isDisplayExportExcelBtn = claimsPrincipal.hasClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_20__["claimResource"].ExportAffInfoExcel);
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    reportTitle: reportTitle,
    param: param,
    isDisplayExportExcelBtn: isDisplayExportExcelBtn,
    isModifiable: isModifiable,
    isClickSearch: isClickSearch,
    dataSummary: dataSummary,
    selectedRequestId: selectedRequestId
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    toggleLoader: _actions_loaderAction__WEBPACK_IMPORTED_MODULE_14__["toggleLoader"],
    deleteProcessingRequest: _actions_index__WEBPACK_IMPORTED_MODULE_23__["deleteProcessingRequest"],
    fetchData: _actions_index__WEBPACK_IMPORTED_MODULE_23__["fetchData"],
    setSelectedRequestId: _actions_index__WEBPACK_IMPORTED_MODULE_23__["setSelectedRequestId"],
    getProcessingRequestDetail: _actions_index__WEBPACK_IMPORTED_MODULE_23__["getProcessingRequestDetail"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/EditRequest/EditRequest.jsx":
/*!**************************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/EditRequest/EditRequest.jsx ***!
  \**************************************************************************************************************/
/*! exports provided: EditRequest, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EditRequest", function() { return EditRequest; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../../actions */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/index.js");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../../libs/constants */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/constants.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ../../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _modules_utils__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ../../../../../modules/utils */ "./Areas/Portal/www/source/js/modules/utils.js");
/* harmony import */ var _reducers__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ../../reducers */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/reducers/index.js");
/* harmony import */ var _libs_service__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ../../libs/service */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/service.js");














function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_11___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_11___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_10___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }











var paymentMethodList = [{
  key: _libs_constants__WEBPACK_IMPORTED_MODULE_18__["playerMethod"],
  value: '12BET player account'
}, {
  key: _libs_constants__WEBPACK_IMPORTED_MODULE_18__["wireMethod"],
  value: 'Bank Transfer'
}];
var EditRequest = /*#__PURE__*/function (_React$PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_9___default()(EditRequest, _React$PureComponent);

  var _super = _createSuper(EditRequest);

  function EditRequest(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_6___default()(this, EditRequest);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleSave", /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_5___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3___default.a.mark(function _callee() {
      var _this$props, selectedRequest, selectedRequestId, validationResult, isInsertedForm, param, result, successMsg, pagingOption, gettingParam;

      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_3___default.a.wrap(function _callee$(_context) {
        while (1) {
          switch (_context.prev = _context.next) {
            case 0:
              _this$props = _this.props, selectedRequest = _this$props.selectedRequest, selectedRequestId = _this$props.selectedRequestId;
              validationResult = Object(_libs_service__WEBPACK_IMPORTED_MODULE_22__["validate"])(selectedRequest);
              isInsertedForm = !selectedRequestId;

              if (validationResult.isValid) {
                _context.next = 7;
                break;
              }

              _this.setState({
                isShowMessage: !validationResult.isValid,
                isSuccess: validationResult.isValid,
                message: validationResult.messageList
              });

              _context.next = 24;
              break;

            case 7:
              _this.props.toggleLoader(true);

              param = selectedRequest;

              if (!isInsertedForm) {
                _context.next = 15;
                break;
              }

              _context.next = 12;
              return _this.props.insertProcessingRequest(param);

            case 12:
              _context.t0 = _context.sent;
              _context.next = 18;
              break;

            case 15:
              _context.next = 17;
              return _this.props.updateProcessingRequest(param);

            case 17:
              _context.t0 = _context.sent;

            case 18:
              result = _context.t0;
              successMsg = isInsertedForm ? _libs_constants__WEBPACK_IMPORTED_MODULE_18__["resource"].InsertSuccess : _libs_constants__WEBPACK_IMPORTED_MODULE_18__["resource"].UpdateSuccess;

              if (result.IsSuccessful) {
                window.setTimeout(_this.props.onClose, 3000);
                pagingOption = _this.props.pagingOption;
                gettingParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default()({}, {
                  PageIndex: pagingOption.PageIndex,
                  PageSize: pagingOption.PageSize
                });

                _this.props.fetchData(gettingParam);
              }

              _this.props.onSave && _this.props.onSave();

              _this.setState({
                isShowMessage: true,
                isSuccess: result.IsSuccessful,
                message: [result.IsSuccessful ? successMsg : result.Message]
              });

              _this.props.toggleLoader(false);

            case 24:
            case "end":
              return _context.stop();
          }
        }
      }, _callee);
    })));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChangePaymentMethod", function (event) {
      var selectedRequest = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default()({}, _this.props.selectedRequest, {
        PreferredPayment: event.target.value
      });

      _this.props.setSelectedRequest(selectedRequest);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "handleChange", function (event) {
      var target = event.target;

      var selectedRequest = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default()({}, _this.props.selectedRequest, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()({}, target.name, target.value));

      _this.props.setSelectedRequest(selectedRequest);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "validateMaxLength", function (text, maxLength) {
      if (!!text) {
        return text.length > maxLength;
      }

      return false;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "validateRequire", function (text) {
      return !text;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_8___default()(_this), "renderTextArea", function (fieldName, labelName, value, isReadOnly, isRequired) {
      var text = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(value);
      var classRequired = isRequired && "required";
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        sm: 3,
        for: fieldName,
        className: classRequired
      }, labelName), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 9
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        type: "textarea",
        id: fieldName,
        name: fieldName,
        value: text,
        onChange: _this.handleChange,
        invalid: _this.validateMaxLength(text, 1000) || isRequired && _this.validateRequire(text),
        required: isRequired,
        disabled: isReadOnly
      }), isRequired && _this.validateRequire(text) && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormFeedback"], null, _libs_constants__WEBPACK_IMPORTED_MODULE_18__["resource"].FieldRequeired), _this.validateMaxLength(text, 1000) && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormFeedback"], null, _libs_constants__WEBPACK_IMPORTED_MODULE_18__["resource"].ExceedMaxLength + ' 1000')));
    });

    _this.state = {
      isShowMessage: false,
      isSuccess: true,
      message: []
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_7___default()(EditRequest, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      if (!this.props.selectedRequestId) {
        var selectedRequest = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default()({}, this.props.selectedRequest, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()({}, 'Status', "Draft"));

        this.props.setSelectedRequest(selectedRequest);
      }
    }
  }, {
    key: "componentWillUnmount",
    value: function componentWillUnmount() {
      this.props.setSelectedRequest(null);
      this.props.setSelectedRequestId(null);
      this.props.setOriginalStatus("Draft");
    }
  }, {
    key: "_renderFormByMethod",
    value: function _renderFormByMethod(selectedRequest) {
      var _ref2;

      return _ref2 = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_ref2, _libs_constants__WEBPACK_IMPORTED_MODULE_18__["playerMethod"], this._renderPaymentForm('playerUsername', '12BET User Name', selectedRequest && selectedRequest.PlayerUsername, false, this.handleChange)), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_12___default()(_ref2, _libs_constants__WEBPACK_IMPORTED_MODULE_18__["wireMethod"], this._renderWireForm(selectedRequest)), _ref2;
    }
  }, {
    key: "_renderPaymentForm",
    value: function _renderPaymentForm(name, label, inputValue, isRequired, changeHandler) {
      var isReadOnly = this.props.isReadOnly;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        for: name,
        sm: 3,
        className: isRequired ? "required" : ""
      }, label), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        type: "text",
        id: name,
        name: name,
        value: Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(inputValue),
        disabled: isReadOnly,
        onChange: changeHandler
      })));
    }
  }, {
    key: "_renderWireForm",
    value: function _renderWireForm(selectedRequest) {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_13___default.a.Fragment, null, this._renderPaymentForm('bankName', 'Bank Name', Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.BankName), false, this.handleChange), this._renderPaymentForm('bankAddress', 'Bank Address', Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.BankAddress), false, this.handleChange), this._renderPaymentForm('bankCode', 'Bank ABA/Swift/Sort Code', Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.BankCode), false, this.handleChange), this._renderPaymentForm('bankAccountName', 'Bank Account Name', Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.BankAccountName), false, this.handleChange), this._renderPaymentForm('bankAccountNumber', 'Bank Account Number', Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.BankAccountNumber), false, this.handleChange));
    }
  }, {
    key: "render",
    value: function render() {
      var _context2;

      var _this$props2 = this.props,
          selectedRequest = _this$props2.selectedRequest,
          isReadOnly = _this$props2.isReadOnly,
          languageList = _this$props2.languageList,
          originalStatus = _this$props2.originalStatus;
      var _this$state = this.state,
          isShowMessage = _this$state.isShowMessage,
          isSuccess = _this$state.isSuccess,
          message = _this$state.message;

      var renderFormByMethod = this._renderFormByMethod(selectedRequest);

      var deadline = Object(_modules_utils__WEBPACK_IMPORTED_MODULE_20__["formatDateTime"])(Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.Deadline), 'yyyy-MM-dd');
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("div", {
        className: "request-container"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message.join('<br>')
        }
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Form"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        for: "UserName",
        className: "required pt-0",
        sm: 2
      }, "Affiliate "), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 4
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        type: "text",
        name: "UserName",
        id: "UserName",
        value: Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["htmlDecode"])(Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_19__["trimText"])(selectedRequest && selectedRequest.UserName)),
        onChange: this.handleChange,
        invalid: this.validateMaxLength(selectedRequest && selectedRequest.UserName, 30) || this.validateRequire(selectedRequest && selectedRequest.UserName)
      }), this.validateRequire(selectedRequest && selectedRequest.UserName) && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormFeedback"], null, _libs_constants__WEBPACK_IMPORTED_MODULE_18__["resource"].FieldRequeired), this.validateMaxLength(selectedRequest && selectedRequest.UserName, 30) && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormFeedback"], null, _libs_constants__WEBPACK_IMPORTED_MODULE_18__["resource"].ExceedMaxLength + ' 30'))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("fieldset", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("legend", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("h6", null, "Player's Details Amendent")), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        sm: 3
      }, "Language"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        type: "select",
        name: "MarketId",
        id: "MarketId",
        value: selectedRequest && selectedRequest.MarketId,
        onChange: this.handleChange
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("option", {
        key: 'status-all',
        value: ""
      }, "No change"), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(languageList).call(languageList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("option", {
          key: 'sortBy-' + item.LanguageCode,
          value: item.LanguageCode
        }, item.LanguageName);
      })))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        sm: 3
      }, "Payment Method"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        type: "select",
        name: "PreferredPayment",
        id: "preferredPayment",
        sm: 6,
        onChange: this.handleChangePaymentMethod
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("option", {
        key: "no-change",
        value: ""
      }, "No change"), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(paymentMethodList).call(paymentMethodList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("option", {
          key: item.key,
          value: item.key
        }, item.value);
      })))), selectedRequest && selectedRequest.PreferredPayment && renderFormByMethod[selectedRequest.PreferredPayment]), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("fieldset", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("length", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("h6", null, "Affiliate Promotion Method & Detail")), this.renderTextArea('PromotionMethod', 'Promotion Method', selectedRequest && selectedRequest.PromotionMethod, isReadOnly), this.renderTextArea('WebRank', 'Web Rank', selectedRequest && selectedRequest.WebRank, isReadOnly), this.renderTextArea('WebRegDate', 'Web Register Date', selectedRequest && selectedRequest.WebRegDate, isReadOnly), this.renderTextArea('WebSMLanguage', 'Web/SM Language', selectedRequest && selectedRequest.WebSMLanguage, isReadOnly), this.renderTextArea('ServerCountry', 'Country (Server)', selectedRequest && selectedRequest.ServerCountry, isReadOnly), this.renderTextArea('WebSMQuality', 'Quality of Website/SM', selectedRequest && selectedRequest.WebSMQuality, isReadOnly), this.renderTextArea('WebSMType', 'Type of Website/SM', selectedRequest && selectedRequest.WebSMType, isReadOnly), this.renderTextArea('WebAdvAff', 'Adv/Aff on Website/SM', selectedRequest && selectedRequest.WebAdvAff, isReadOnly), this.renderTextArea('TargetMarket', 'Target Market', selectedRequest && selectedRequest.TargetMarket, isReadOnly)), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("fieldset", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("legend", null, "Final Verdict by Agent"), this.renderTextArea('AgentName', 'Agent Name', selectedRequest && selectedRequest.AgentName, isReadOnly, true), this.renderTextArea('ContactModeTime', 'Contactable Mode & Time', selectedRequest && selectedRequest.ContactModeTime, isReadOnly, false), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        sm: 3,
        for: "Status"
      }, "Status"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 9
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        sm: 9,
        type: "select",
        id: "Status",
        name: "Status",
        onChange: this.handleChange,
        value: selectedRequest && selectedRequest.Status
      }, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(_context2 = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default()(_libs_constants__WEBPACK_IMPORTED_MODULE_18__["statusList"])).call(_context2, function (key) {
        return _libs_constants__WEBPACK_IMPORTED_MODULE_18__["statusList"][key].shouldShow(originalStatus) ? /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("option", {
          key: key,
          value: key
        }, key) : '';
      })))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["FormGroup"], {
        row: true
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Label"], {
        sm: 3,
        className: "required pt-0",
        for: "Deadline"
      }, "Deadline"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Col"], {
        sm: 4
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_16__["Input"], {
        type: "date",
        id: "Deadline",
        name: "Deadline",
        value: deadline,
        required: true,
        onChange: this.handleChange
      }))), this.renderTextArea('Remark', 'Remark', selectedRequest && selectedRequest.Remark, isReadOnly)), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_13___default.a.createElement("button", {
        type: "button",
        className: "my-2 btn btn-success btn-save",
        onClick: this.handleSave,
        disabled: isReadOnly
      }, "SAVE")));
    }
  }]);

  return EditRequest;
}(react__WEBPACK_IMPORTED_MODULE_13___default.a.PureComponent);

function mapStateToProp(_ref3) {
  var selectedRequest = _ref3.selectedRequest,
      selectedRequestId = _ref3.selectedRequestId,
      languageList = _ref3.languageList,
      originalStatus = _ref3.originalStatus;
  return {
    selectedRequest: selectedRequest,
    selectedRequestId: selectedRequestId,
    languageList: languageList,
    originalStatus: originalStatus
  };
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_15__["bindActionCreators"])({
    setSelectedRequest: _actions__WEBPACK_IMPORTED_MODULE_17__["setSelectedRequest"],
    setSelectedRequestId: _actions__WEBPACK_IMPORTED_MODULE_17__["setSelectedRequestId"],
    getProcessingRequestDetail: _actions__WEBPACK_IMPORTED_MODULE_17__["getProcessingRequestDetail"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_17__["fetchData"],
    toggleLoader: _actions__WEBPACK_IMPORTED_MODULE_17__["toggleLoader"],
    updateProcessingRequest: _actions__WEBPACK_IMPORTED_MODULE_17__["updateProcessingRequest"],
    insertProcessingRequest: _actions__WEBPACK_IMPORTED_MODULE_17__["insertProcessingRequest"],
    setOriginalStatus: _actions__WEBPACK_IMPORTED_MODULE_17__["setOriginalStatus"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_14__["connect"])(mapStateToProp, mapDispatchToProps)(EditRequest));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/Filter.jsx":
/*!*********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/Filter.jsx ***!
  \*********************************************************************************************/
/*! exports provided: Filter, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Filter", function() { return Filter; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2__);
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/index.js");












function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }







var Filter = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(Filter, _PureComponent);

  var _super = _createSuper(Filter);

  function Filter(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Filter);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleChange", function (event) {
      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, event.target.name, event.target.value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handlePressEnter", function (event) {
      if (event.charCode === 13) {
        _this.handleClickSearch();
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleClickSearch", function () {
      var pagingOption = _this.props.pagingOption;
      var _this$state = _this.state,
          selectedStatus = _this$state.selectedStatus,
          userName = _this$state.userName;

      _this.props.setReportTitle(_this._buildReportTitle(selectedStatus, userName));

      var param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
        UserName: userName,
        Status: selectedStatus
      }, {
        PageIndex: 1,
        PageSize: pagingOption.PageSize
      });

      _this.props.setParam(param);

      _this.props.setIsClickSearch(true);

      _this.props.fetchData(param);
    });

    _this.state = {
      userName: '',
      selectedStatus: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Filter, [{
    key: "_buildReportTitle",
    value: function _buildReportTitle(selectedStatus, userName) {
      var space = _libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].Space;
      var apostrophe = _libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].Apostrophe;
      var statusTitle = _libs_constants__WEBPACK_IMPORTED_MODULE_15__["statusList"][selectedStatus] || _libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].AllStatuses;
      var titles = [_libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].ReportName];
      userName && titles.push(_libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].UsernameContains + apostrophe + userName + apostrophe);
      titles.push(statusTitle);
      return titles.join(space);
    }
  }, {
    key: "render",
    value: function render() {
      var _context;

      var selectedStatus = this.state.selectedStatus;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "filter-container"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Form"], {
        inline: true,
        tag: "div"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "my-2 mr-sm-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "text",
        name: "userName",
        id: "Username",
        placeholder: "Username",
        onKeyPress: this.handlePressEnter,
        onChange: this.handleChange
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "my-2 mr-sm-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "selectedStatus",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].SortBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "selectedStatus",
        id: "selectedStatus",
        value: selectedStatus,
        onChange: this.handleChange
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'status-all',
        value: ""
      }, "All Status"), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(_context = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default()(_libs_constants__WEBPACK_IMPORTED_MODULE_15__["statusList"])).call(_context, function (key) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: 'sortBy-' + key,
          value: key
        }, key);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        className: "my-2",
        outline: true,
        color: "primary",
        type: "button",
        onClick: this.handleClickSearch
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-magnifying-glass"
      }), " ", _libs_constants__WEBPACK_IMPORTED_MODULE_15__["resource"].Search)));
    }
  }]);

  return Filter;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref) {
  var param = _ref.param,
      pagingOption = _ref.pagingOption;
  return {
    param: param,
    pagingOption: pagingOption
  };
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setReportTitle: _actions__WEBPACK_IMPORTED_MODULE_16__["setReportTitle"],
    setParam: _actions__WEBPACK_IMPORTED_MODULE_16__["setParam"],
    setIsClickSearch: _actions__WEBPACK_IMPORTED_MODULE_16__["setIsClickSearch"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_16__["fetchData"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Filter));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/GridData.jsx":
/*!***********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/GridData.jsx ***!
  \***********************************************************************************************/
/*! exports provided: GridData, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GridData", function() { return GridData; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/index.js");
/* harmony import */ var _modules_utils__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../../../modules/utils */ "./Areas/Portal/www/source/js/modules/utils.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");










function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_7___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_6___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }












var GridData = /*#__PURE__*/function (_Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_5___default()(GridData, _Component);

  var _super = _createSuper(GridData);

  function GridData(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default()(this, GridData);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4___default()(_this), "handleDeleteProcess", function (selectedId) {
      _this.props.setSelectedRequestId(selectedId);

      _this.props.handleDelete();
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4___default()(_this), "handleEditProcess", function (selectedId) {
      _this.props.setSelectedRequestId(selectedId);

      _this.props.handleEdit();
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4___default()(_this), "handleViewProcess", function (selectedId) {
      _this.props.setSelectedRequestId(seletedId);

      _this.props.handleView();
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_4___default()(_this), "handlePaging", function (index, size) {
      var newParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, _this.props.param, {
        PageIndex: index,
        PageSize: size
      });

      _this.props.fetchData(newParam);
    });

    var today = new Date(_this.props.today);
    _this.state = {
      todayTime: today.getTime(),
      messageNotification: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default()(GridData, [{
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var param = this.props.param;

      if (param && param !== prevProps.param) {
        var pagingOption = this.props.pagingOption;

        var updateParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, param, {
          PageIndex: 1,
          PageSize: pagingOption.PageSize
        });

        this.props.fetchData(updateParam);
      }
    }
  }, {
    key: "_renderHeader",
    value: function _renderHeader() {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Row"], {
        key: "theard"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 50,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Index
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Index), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].UserName
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].UserName), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].CreatedBy
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].CreatedBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].CreationDate
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].CreationDate), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].LastModified
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].LastModified), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].SubmittedDate
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].SubmittedDate), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Deadline
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Deadline), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].ApprovedBy
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].ApprovedBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].ApprovalDate
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].ApprovalDate), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 100,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Status
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Status), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        header: true,
        colWidth: 80,
        title: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Action
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["headerText"].Action));
    }
  }, {
    key: "_renderDataSection",
    value: function _renderDataSection(dataList) {
      var dataRows = [];

      if (dataList && dataList.length) {
        var todayTime = this.state.todayTime;
        var pagingOption = this.props.pagingOption;
        var startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (var i = 0; i < dataList.length; i++) {
          var dataItem = dataList[i];
          dataRows.push(this._buildDataRow(dataItem, i + startIndex, todayTime));
        }
      } else {
        dataRows.push( /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Row"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
          className: "text-center",
          colSpan: "11"
        }, _libs_constants__WEBPACK_IMPORTED_MODULE_14__["globalResource"].NoInfo)));
      }

      return dataRows;
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(dataItem, index, todayTime) {
      var _this2 = this;

      var claimsPrincipal = this.props.claimsPrincipal;
      var status = dataItem.Status;
      var deadline = dataItem.Deadline;
      var deadlineDate = new Date(deadline);
      var isShowWarnning = deadlineDate.getTime() < todayTime && status === _libs_constants__WEBPACK_IMPORTED_MODULE_14__["statusList"].Submitted;
      var isReadOnlyClaim = new _modules_claim__WEBPACK_IMPORTED_MODULE_17__["Claim"](_modules_claim__WEBPACK_IMPORTED_MODULE_17__["claimResource"].ProcRequest, '');
      var isViewOnly = claimsPrincipal.hasClaim(isReadOnlyClaim) || claimsPrincipal.hasWriteClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_17__["claimResource"].ProcRequestMarketingRole) && !claimsPrincipal.hasWriteClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_17__["claimResource"].ProcRequestManagementRole) && dataItem.Status === 'Draft';
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Row"], {
        key: "dataRow-".concat(index)
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        className: "text-center",
        title: index
      }, index), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.UserName
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("a", {
        href: Object(_modules_utils__WEBPACK_IMPORTED_MODULE_16__["resolveClientUrl"])("Portal/Customer?user=".concat(dataItem.UserName)),
        target: "_top"
      }, dataItem.UserName)), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.CreatedBy
      }, dataItem.CreatedBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.CreationDate,
        className: "text-center"
      }, Object(_modules_utils__WEBPACK_IMPORTED_MODULE_16__["formatDateTime"])(dataItem.CreationDate, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["defaultDatetimeFormat"])), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.LastModified,
        className: "text-center"
      }, Object(_modules_utils__WEBPACK_IMPORTED_MODULE_16__["formatDateTime"])(dataItem.LastModified, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["defaultDatetimeFormat"])), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.SubmittedDate,
        className: "text-center"
      }, Object(_modules_utils__WEBPACK_IMPORTED_MODULE_16__["formatDateTime"])(dataItem.SubmittedDate, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["defaultDatetimeFormat"])), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        className: isShowWarnning ? "text-danger text-center" : "text-center",
        title: dataItem.Deadline
      }, Object(_modules_utils__WEBPACK_IMPORTED_MODULE_16__["formatDateTime"])(dataItem.Deadline, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["defaultDatetimeFormat"])), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.ApprovedBy
      }, dataItem.ApprovedBy), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.ApprovalDate,
        className: "text-center"
      }, Object(_modules_utils__WEBPACK_IMPORTED_MODULE_16__["formatDateTime"])(dataItem.ApprovalDate, _modules_constants__WEBPACK_IMPORTED_MODULE_18__["defaultDatetimeFormat"])), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        title: dataItem.Status,
        className: "text-center",
        style: {
          color: _libs_constants__WEBPACK_IMPORTED_MODULE_14__["colorByStatus"][status]
        }
      }, status), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Cell"], {
        className: "text-center",
        title: "Action"
      }, isViewOnly ? /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Button"], {
        color: "link",
        onClick: function onClick() {
          return _this2.handleViewProcess(dataItem.RequestId);
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("span", {
        className: "oi oi-eye text-primary"
      })) : /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_9___default.a.Fragment, null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Button"], {
        color: "link",
        onClick: function onClick() {
          return _this2.handleEditProcess(dataItem.RequestId);
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("span", {
        className: "oi oi-pencil text-secondary"
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Button"], {
        color: "link",
        onClick: function onClick() {
          return _this2.handleDeleteProcess(dataItem.RequestId);
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("span", {
        className: "oi oi-trash text-danger"
      })))));
    }
  }, {
    key: "render",
    value: function render() {
      var messageNotification = this.state.messageNotification;
      var _this$props = this.props,
          gridData = _this$props.gridData,
          shouldResetScrollPosition = _this$props.shouldResetScrollPosition;

      if (!gridData) {
        return null;
      }

      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_9___default.a.Fragment, null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
        className: "table-responsive table-striped"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_13__["Table"], {
        minWidth: 1000,
        autoWidth: true,
        bodyHeight: 300,
        header: this._renderHeader(),
        body: this._renderDataSection(gridData),
        containerPadding: 20,
        isPaging: true,
        pageOption: this.props.pagingOption,
        onPaging: this.handlePaging,
        shouldResetScrollPosition: shouldResetScrollPosition
      })));
    }
  }]);

  return GridData;
}(react__WEBPACK_IMPORTED_MODULE_9__["Component"]);

function mapStateToProps(_ref) {
  var today = _ref.today,
      gridData = _ref.gridData,
      pagingOption = _ref.pagingOption,
      shouldResetScrollPosition = _ref.shouldResetScrollPosition,
      claimsPrincipal = _ref.claimsPrincipal;
  return {
    today: today,
    gridData: gridData,
    pagingOption: pagingOption,
    shouldResetScrollPosition: shouldResetScrollPosition,
    claimsPrincipal: claimsPrincipal
  };
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_11__["bindActionCreators"])({
    setPagingOption: _actions__WEBPACK_IMPORTED_MODULE_15__["setPagingOption"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_15__["fetchData"],
    setSelectedRequestId: _actions__WEBPACK_IMPORTED_MODULE_15__["setSelectedRequestId"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_10__["connect"])(mapStateToProps, mapDispatchToProps)(GridData));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/index.jsx":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/index.jsx ***!
  \*********************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return App; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var _reducers__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./reducers */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/reducers/index.js");
/* harmony import */ var _components_AppContainer__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./components/AppContainer */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/components/AppContainer.jsx");
/* harmony import */ var _modules_store__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../../../modules/store */ "./Areas/Portal/www/source/js/modules/store.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");








function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }








var App = /*#__PURE__*/function (_Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_4___default()(App, _Component);

  var _super = _createSuper(App);

  function App(props) {
    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default()(this, App);

    return _super.call(this, props);
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default()(App, [{
    key: "render",
    value: function render() {
      var claimsPrincipal = _modules_claim__WEBPACK_IMPORTED_MODULE_12__["default"].currentClaimPrincipal;

      var state = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, this.state, {
        claimsPrincipal: claimsPrincipal
      }, window.initialState);

      var store = Object(_modules_store__WEBPACK_IMPORTED_MODULE_11__["configureStore"])(state, _reducers__WEBPACK_IMPORTED_MODULE_9__["default"]);
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(react_redux__WEBPACK_IMPORTED_MODULE_8__["Provider"], {
        store: store
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_components_AppContainer__WEBPACK_IMPORTED_MODULE_10__["default"], null));
    }
  }]);

  return App;
}(react__WEBPACK_IMPORTED_MODULE_7__["Component"]);



/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/constants.js":
/*!*****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/constants.js ***!
  \*****************************************************************************************/
/*! exports provided: globalResource, colorByStatus, statusList, headerText, resource, playerMethod, netellerMethod, moneybookersMethod, wireMethod */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "colorByStatus", function() { return colorByStatus; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "statusList", function() { return statusList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "headerText", function() { return headerText; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "playerMethod", function() { return playerMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "netellerMethod", function() { return netellerMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "moneybookersMethod", function() { return moneybookersMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "wireMethod", function() { return wireMethod; });
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_0__["globalResource"]; });



var colorByStatus = {
  Draft: "#000000",
  Submitted: "#0000FF",
  Approved: "#008000",
  Rejected: "#FF0000"
};
var statusList = {
  'Draft': {
    value: 1,
    shouldShow: function shouldShow(status) {
      return !status || status === 'Draft';
    }
  },
  'Submitted': {
    value: 2,
    shouldShow: function shouldShow(status) {
      return !status || status === 'Draft' || status === 'Submitted';
    }
  },
  'Approved': {
    value: 3,
    shouldShow: function shouldShow(status) {
      return !!status && status !== 'Draft';
    }
  },
  'Rejected': {
    value: 4,
    shouldShow: function shouldShow(status) {
      return !!status && status !== 'Draft';
    }
  }
};
var headerText = {
  Index: _modules_globalResource__WEBPACK_IMPORTED_MODULE_0__["globalResource"].Index,
  UserName: 'Username',
  CreatedBy: 'Created By',
  CreationDate: 'Creation Date',
  LastModified: 'Last Modified',
  SubmittedDate: 'Sumitted',
  Deadline: 'Deadline',
  ApprovedBy: 'Approved By',
  ApprovalDate: 'Approval Date',
  Status: 'Status',
  Action: 'Action'
};
var resource = {
  Search: _modules_globalResource__WEBPACK_IMPORTED_MODULE_0__["globalResource"].Search,
  Space: ' - ',
  ReportName: 'PROCESSING REQUEST REPORT',
  AllStatuses: 'All STATUSES',
  UsernameContains: 'USERNAME CONTAINS ',
  StatusCount: 'Status count',
  Apostrophe: "'",
  ExceedMaxLength: 'Exceed max length',
  FieldRequeired: 'This field is required.',
  InsertSuccess: 'Insert successful',
  UpdateSuccess: 'Update successfult'
};
var playerMethod = 'Player';
var netellerMethod = 'Neteller';
var moneybookersMethod = 'Moneybookers';
var wireMethod = 'Wire';

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/reportExcel.js":
/*!*******************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/reportExcel.js ***!
  \*******************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return ReportExcel; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/reduce */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _modules_excel__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../modules/excel */ "./Areas/Portal/www/source/js/modules/excel.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../../../modules/excelStyleConstant */ "./Areas/Portal/www/source/js/modules/excelStyleConstant.js");
/* harmony import */ var _constants__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./constants */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/constants.js");
/* harmony import */ var _modules_numberFormat__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../../../../modules/numberFormat */ "./Areas/Portal/www/source/js/modules/numberFormat.js");










var REPORT_TITLE_ROW_INDEX = 0;
var HEADER_ROW_INDEX = 1;
var DATA_ROW_INDEX = 2;
var TOTAL_COLUMN = 10;
var MAIN_SECTION_STYLE = 'mainSectionStyle';
var fields = {
  Index: 'Index',
  UserName: 'UserName',
  CreatedBy: 'CreatedBy',
  CreationDate: 'CreationDate',
  LastModified: 'LastModified',
  Submitted: 'SubmittedDate',
  Deadline: 'Deadline',
  ApprovedBy: 'ApprovedBy',
  ApprovalDate: 'ApprovalDate',
  Status: 'Status'
};

var ReportExcel = /*#__PURE__*/function () {
  function ReportExcel(data, title) {
    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default()(this, ReportExcel);

    this.title = title;
    this.data = data;
    this.worksheet = {};
    this.currentRowIndex = DATA_ROW_INDEX;
    this.mergeConfig = [];
    this.columnConfigs = this._getcolumnConfigs();
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default()(ReportExcel, [{
    key: "export",
    value: function _export() {
      var _this = this;

      _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].process(function () {
        _this._createReportTitle();

        _this._createHeader();

        _this._createBodyData();

        _this._createExcelConfig();

        _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].export(_this.worksheet, _this.title);
      });
    }
  }, {
    key: "_createReportTitle",
    value: function _createReportTitle() {
      this.mergeConfig.push({
        s: {
          r: REPORT_TITLE_ROW_INDEX,
          c: 0
        },
        e: {
          r: REPORT_TITLE_ROW_INDEX,
          c: 10
        }
      });
      var address = _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].getCellAddress({
        c: 0,
        r: REPORT_TITLE_ROW_INDEX
      });
      this.worksheet[address] = {
        t: "s",
        v: this.title,
        s: _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["titleCellStyle"]
      };
    }
  }, {
    key: "_createHeader",
    value: function _createHeader() {
      var columnIndex = 0;

      for (var i = 0; i < TOTAL_COLUMN; i++) {
        var fieldName = this.columnConfigs[i].field;
        var address = _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].getCellAddress({
          c: columnIndex,
          r: HEADER_ROW_INDEX
        });
        this.worksheet[address] = {
          t: "s",
          v: _constants__WEBPACK_IMPORTED_MODULE_8__["headerText"][fieldName],
          s: _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["headerCellStyle"]
        };
        columnIndex++;
      }
    }
  }, {
    key: "_createBodyData",
    value: function _createBodyData() {
      var mainData = this.data.MainData;

      for (var i = 0; i < mainData.length; i++) {
        var item = mainData[i];
        var columnIndex = 0;

        for (var j = 0; j < TOTAL_COLUMN; j++) {
          var columnConfig = this.columnConfigs[j];
          var address = _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].getCellAddress({
            c: columnIndex,
            r: this.currentRowIndex
          });
          this.worksheet[address] = this._buildDataRow(item, MAIN_SECTION_STYLE, columnConfig, i);
          columnIndex++;
        }

        this.currentRowIndex++;
      }
    }
  }, {
    key: "_buildDataRow",
    value: function _buildDataRow(item, sectionStyleName, columnConfig, rowIndex) {
      var fieldName = columnConfig.field;
      var value = fieldName === fields.Index ? rowIndex + 1 : item[fieldName];
      return {
        t: _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].getCellType(value),
        v: Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_6__["htmlDecode"])(value),
        s: columnConfig[sectionStyleName]
      };
    }
  }, {
    key: "_createExcelConfig",
    value: function _createExcelConfig() {
      var _context, _context2;

      var cellStart = _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].getCellAddress({
        r: 0,
        c: 0
      });
      var cellEnd = _modules_excel__WEBPACK_IMPORTED_MODULE_5__["default"].getCellAddress({
        r: this.currentRowIndex,
        c: TOTAL_COLUMN
      });
      this.worksheet["!merges"] = this.mergeConfig;
      this.worksheet["!ref"] = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context = "".concat(cellStart, ":")).call(_context, cellEnd);
      this.worksheet['!cols'] = _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1___default()(_context2 = this.columnConfigs).call(_context2, function (result, currentItem) {
        result.push({
          wpx: currentItem.width
        });
        return result;
      }, []);
    }
  }, {
    key: "_getcolumnConfigs",
    value: function _getcolumnConfigs() {
      var _ref, _ref2, _ref3, _ref4, _ref5, _ref6, _ref7, _ref8, _ref9, _ref10;

      return [(_ref = {
        field: fields.Index
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].right), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref, "width", 70), _ref), (_ref2 = {
        field: fields.UserName
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref2, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref2, "width", 200), _ref2), (_ref3 = {
        field: fields.CreatedBy
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref3, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref3, "width", 150), _ref3), (_ref4 = {
        field: fields.CreationDate
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref4, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref4, "width", 150), _ref4), (_ref5 = {
        field: fields.LastModified
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref5, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref5, "width", 170), _ref5), (_ref6 = {
        field: fields.Submitted
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref6, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref6, "width", 170), _ref6), (_ref7 = {
        field: fields.Deadline
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref7, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref7, "width", 150), _ref7), (_ref8 = {
        field: fields.ApprovedBy
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref8, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref8, "width", 150), _ref8), (_ref9 = {
        field: fields.ApprovalDate
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref9, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref9, "width", 250), _ref9), (_ref10 = {
        field: fields.Status
      }, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref10, MAIN_SECTION_STYLE, _modules_excelStyleConstant__WEBPACK_IMPORTED_MODULE_7__["bodyCellStyle"].left), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()(_ref10, "width", 200), _ref10)];
    }
  }]);

  return ReportExcel;
}();



/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/service.js":
/*!***************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/libs/service.js ***!
  \***************************************************************************************/
/*! exports provided: validate */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "validate", function() { return validate; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/for-each */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/for-each.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_readOnlyError__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/readOnlyError */ "./node_modules/@babel/runtime-corejs3/helpers/readOnlyError.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_readOnlyError__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_readOnlyError__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/toConsumableArray */ "./node_modules/@babel/runtime-corejs3/helpers/toConsumableArray.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");





var validate = function validate(selectedRequest) {
  var _context;

  var valRequireFieldResult = validateRequireField(selectedRequest);
  var valMaxLengthResult = validateMaxLength(selectedRequest);
  var valDeadlineResult = validateDeadline(selectedRequest);

  var messageList = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_2___default()(_context = []).call(_context, _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_3___default()(valRequireFieldResult.messageList), _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_3___default()(valMaxLengthResult.messageList), _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_3___default()(valDeadlineResult.messageList));

  var isValid = messageList.length === 0;
  return {
    isValid: isValid,
    messageList: messageList
  };
};

var validateRequireField = function validateRequireField(selectedRequest) {
  var userName = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_4__["trimText"])(selectedRequest && selectedRequest.UserName);
  var agentName = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_4__["trimText"])(selectedRequest && selectedRequest.AgentName);
  var isValid = true;
  var messageList = [];

  if (userName.length === 0 || agentName.length === 0) {
    isValid = false;
    messageList.push("Not fill all require field");
  }

  return {
    isValid: isValid,
    messageList: messageList
  };
};

var validateMaxLength = function validateMaxLength(selectedRequest) {
  var messageList = [];
  var isValid = true;
  var textAreaNames = ['AgentName', 'PromotionMethod', 'Remark', 'ServerCountry', 'TargetMarket', 'WebAdvAff', 'WebRank', 'WebRegDate', 'WebSMLanguage', 'WebSMQuality', 'WebSMType'];
  var userName = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_4__["trimText"])(selectedRequest && selectedRequest.UserName);
  var isExceed = checkExceedMaxLength(userName, 30);

  if (isExceed) {
    messageList.push("Affiliate name exceed max charaters length (30)");
  }

  _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_0___default()(textAreaNames).call(textAreaNames, function (name) {
    var text = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_4__["trimText"])(selectedRequest && selectedRequest[name]);
    if (checkExceedMaxLength(text, 1000)) messageList.push("Field ".concat(name, " exceed max length (1000)"));
  });

  if (messageList.length > 0) {
    isValid = (_babel_runtime_corejs3_helpers_readOnlyError__WEBPACK_IMPORTED_MODULE_1___default()("isValid"), false);
  }

  return {
    isValid: isValid,
    messageList: messageList
  };
};

var validateDeadline = function validateDeadline(selectedRequest) {
  var isValid = true;
  var messageList = [];
  var now = new Date();
  var today = new Date();
  var deadline = selectedRequest && selectedRequest.Deadline;

  if (!deadline) {
    messageList.push('Require Deadline');
  } else {
    var date = new Date(deadline);
    date.setDate(date.getDate() + 1);

    if (date < now) {
      messageList.push('Invalid deadline');
    }
  }

  if (messageList.length > 0) {
    isValid = false;
  }

  return {
    isValid: isValid,
    messageList: messageList
  };
};

var checkExceedMaxLength = function checkExceedMaxLength(text, maxLength) {
  return text.length > maxLength;
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/reducers/index.js":
/*!*****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/affiliate/processing-request/reducers/index.js ***!
  \*****************************************************************************************/
/*! exports provided: reportTitle, param, selectedRequestId, selectedRequest, shouldResetScrollPosition, gridData, isClickSearch, dataSummary, languageList, originalStatus, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "reportTitle", function() { return reportTitle; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "param", function() { return param; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "selectedRequestId", function() { return selectedRequestId; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "selectedRequest", function() { return selectedRequest; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "shouldResetScrollPosition", function() { return shouldResetScrollPosition; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "gridData", function() { return gridData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "isClickSearch", function() { return isClickSearch; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "dataSummary", function() { return dataSummary; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "languageList", function() { return languageList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "originalStatus", function() { return originalStatus; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _modules_createReducer__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");
/* harmony import */ var _actions_types__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../actions/types */ "./Areas/Portal/www/source/js/pages/affiliate/processing-request/actions/types.js");
/* harmony import */ var _reducers_pagingReducer__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../reducers/pagingReducer */ "./Areas/Portal/www/source/js/reducers/pagingReducer.js");
/* harmony import */ var _reducers_loaderReducer__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../../../reducers/loaderReducer */ "./Areas/Portal/www/source/js/reducers/loaderReducer.js");
/* harmony import */ var _reducers_claimReducer__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../../../reducers/claimReducer */ "./Areas/Portal/www/source/js/reducers/claimReducer.js");








var today = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(null, {});
var reportTitle = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])('', _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_REPORT_TITLE"], function (prevState, action) {
  return action.data;
}));
var param = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_PARAM"], function (prevState, action) {
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, prevState, action.data);
}));
var selectedRequestId = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(0, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_SELECTED_REQUEST_ID"], function (prevState, action) {
  return action.data;
}));
var selectedRequest = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_SELECTED_PROC_REQUEST"], function (prevState, action) {
  return action.data;
}));
var shouldResetScrollPosition = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(true, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_SHOULD_RESET_SCROLL_POSITION"], function (prevState, action) {
  return action.data;
}));
var gridData = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_GRID_DATA"], function (prevState, action) {
  return action.data;
}));
var isClickSearch = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(false, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_IS_CLICK_SEARCH"], function (prevState, action) {
  return action.data;
}));
var dataSummary = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_PROC_REQUEST_SUMMARY"], function (prevState, action) {
  return action.data;
}));
var languageList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_LANGUAGE_LIST"], function (prevState, action) {
  return action.data;
}));
var originalStatus = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_3__["default"])("Draft", _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_1___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_4__["SET_ORGINAL_STATUS"], function (prev, action) {
  return action.data;
}));
/* harmony default export */ __webpack_exports__["default"] = (Object(redux__WEBPACK_IMPORTED_MODULE_2__["combineReducers"])({
  today: today,
  pagingOption: _reducers_pagingReducer__WEBPACK_IMPORTED_MODULE_5__["pagingOption"],
  reportTitle: reportTitle,
  param: param,
  isShowLoader: _reducers_loaderReducer__WEBPACK_IMPORTED_MODULE_6__["isShowLoader"],
  selectedRequestId: selectedRequestId,
  selectedRequest: selectedRequest,
  gridData: gridData,
  shouldResetScrollPosition: shouldResetScrollPosition,
  claimsPrincipal: _reducers_claimReducer__WEBPACK_IMPORTED_MODULE_7__["claimsPrincipal"],
  isClickSearch: isClickSearch,
  dataSummary: dataSummary,
  languageList: languageList,
  originalStatus: originalStatus
}));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/components/NotificationPopup.jsx":
/*!**********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/components/NotificationPopup.jsx ***!
  \**********************************************************************************/
/*! exports provided: NotificationPopup, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NotificationPopup", function() { return NotificationPopup; });
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");


var widthHeight = {
  width: 70,
  height: 30
};
var NotificationPopup = function NotificationPopup(props) {
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Modal"], {
    isOpen: !!props.message,
    toggle: function toggle() {
      return props.onClose(false);
    },
    backdrop: "static"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["ModalHeader"], {
    toggle: function toggle() {
      return props.onClose(false);
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    className: "text-truncate",
    style: {
      width: 450
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("span", {
    className: "iconpopup"
  }), props.isConfirmPopup ? 'Confirmation' : 'Notification')), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["ModalBody"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    className: "modal-body-wrapper"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    className: "notification-popup-container",
    style: {
      fontSize: 14
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("h5", null, window.location.host, " says"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    dangerouslySetInnerHTML: {
      __html: props.message
    }
  }), props.isConfirmPopup ? /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    className: "mt-5"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Button"], {
    style: widthHeight,
    color: "success mr-3",
    onClick: function onClick() {
      return props.onClose(true);
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("strong", null, "Confirm")), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Button"], {
    style: widthHeight,
    color: "info",
    onClick: function onClick() {
      return props.onClose(false);
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("strong", null, "Cancel"))) : /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Button"], {
    style: widthHeight,
    color: "success mt-5",
    onClick: function onClick() {
      return props.onClose(false);
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("strong", null, "OK"))))));
};
/* harmony default export */ __webpack_exports__["default"] = (/*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(NotificationPopup));

/***/ })

}]);
//# sourceMappingURL=116a3e799a50367edc05.chunk.js.map