(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["userAccount"],{

/***/ "./Areas/Portal/www/source/js/modules/dataHandler.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/dataHandler.js ***!
  \***********************************************************/
/*! exports provided: removeItem, deepClone, updateObjectInArray */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "removeItem", function() { return removeItem; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deepClone", function() { return deepClone; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "updateObjectInArray", function() { return updateObjectInArray; });
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectSpread */ "./node_modules/@babel/runtime-corejs3/helpers/objectSpread.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_json_stringify__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/json/stringify */ "./node_modules/@babel/runtime-corejs3/core-js-stable/json/stringify.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_json_stringify__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_json_stringify__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/filter */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/filter.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_3__);




var removeItem = function removeItem(index, dataList) {
  return _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_3___default()(dataList).call(dataList, function (item, i) {
    return i !== index;
  });
};
var deepClone = function deepClone(data) {
  return JSON.parse(_babel_runtime_corejs3_core_js_stable_json_stringify__WEBPACK_IMPORTED_MODULE_2___default()(data));
};
var updateObjectInArray = function updateObjectInArray(array, indexItem, updatedItem) {
  return _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default()(array).call(array, function (item, index) {
    if (index !== indexItem) {
      return item;
    }

    return _babel_runtime_corejs3_helpers_objectSpread__WEBPACK_IMPORTED_MODULE_0___default()({}, item, updatedItem);
  });
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js":
/*!***********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js ***!
  \***********************************************************************************/
/*! exports provided: getAffiliateInfo, setUserAccountInfo, setMoreSiteUrlsList, getUserAccountInfo, updateUserAccount */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setUserAccountInfo", function() { return setUserAccountInfo; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setMoreSiteUrlsList", function() { return setMoreSiteUrlsList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getUserAccountInfo", function() { return getUserAccountInfo; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "updateUserAccount", function() { return updateUserAccount; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../customer/user-panel/actions */ "./Areas/Portal/www/source/js/pages/customer/user-panel/actions/index.js");
/* harmony import */ var _customer_mainpage_actions_loadingDataAction__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../customer/mainpage/actions/loadingDataAction */ "./Areas/Portal/www/source/js/pages/customer/mainpage/actions/loadingDataAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "getAffiliateInfo", function() { return _customer_mainpage_actions_loadingDataAction__WEBPACK_IMPORTED_MODULE_4__["getAffiliateInfo"]; });

/* harmony import */ var _types__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./types */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/types.js");







var setUserAccountInfo = function setUserAccountInfo(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_5__["SET_USER_ACCOUNT_INFO"],
    data: data
  };
};
var setMoreSiteUrlsList = function setMoreSiteUrlsList(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_5__["SET_MORE_SITE_URLS_LIST"],
    data: data
  };
};
var getUserAccountInfo = function getUserAccountInfo(affId) {
  return (
    /*#__PURE__*/
    function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_1___default()(
      /*#__PURE__*/
      _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(dispatch) {
        var url, response, data, moreSiteUrls, moreSiteUrlsList;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context) {
          while (1) {
            switch (_context.prev = _context.next) {
              case 0:
                dispatch(Object(_customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_3__["toggleLoaderUserPanel"])(true));
                url = 'Portal/UserPanel/UserAccount/GetUserAccountInfo';
                _context.next = 4;
                return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_2__["post"])(url, {
                  affId: affId
                });

              case 4:
                response = _context.sent;
                data = response && response.status === 200 ? response.data : null;
                moreSiteUrls = data && data.MoreSiteUrls;
                moreSiteUrlsList = moreSiteUrls && moreSiteUrls != '' && moreSiteUrls.split(';');
                dispatch(setMoreSiteUrlsList(moreSiteUrlsList));
                dispatch(setUserAccountInfo(data));
                dispatch(Object(_customer_user_panel_actions__WEBPACK_IMPORTED_MODULE_3__["toggleLoaderUserPanel"])(false));

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
    }()
  );
};
var updateUserAccount = function updateUserAccount(param) {
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
              url = 'Portal/UserPanel/UserAccount/updateUserAccount';
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

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/AppContainer.jsx":
/*!**********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/AppContainer.jsx ***!
  \**********************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/Content.jsx");




var AppContainer = function AppContainer(props) {
  return react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_1__["default"], null, react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_2__["default"], null));
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(AppContainer));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/Content.jsx":
/*!*****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/Content.jsx ***!
  \*****************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/reduce */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/trim */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/trim.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_14___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_14__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js");
/* harmony import */ var _modules_validation__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ../modules/validation */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/validation.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/constants.js");
/* harmony import */ var _Section__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./Section */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/Section.jsx");
/* harmony import */ var _sections_Personal__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! ./sections/Personal */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Personal.jsx");
/* harmony import */ var _sections_Additional__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! ./sections/Additional */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Additional.jsx");
/* harmony import */ var _sections_EmailFormat__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! ./sections/EmailFormat */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/EmailFormat.jsx");
/* harmony import */ var _sections_WebSite__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! ./sections/WebSite */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/WebSite.jsx");
/* harmony import */ var _sections_Payment__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! ./sections/Payment */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Payment.jsx");



























var Content =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_12___default()(Content, _PureComponent);

  function Content(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_7___default()(this, Content);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default()(Content).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11___default()(_this), "handleGetMoreSiteUrls", function (moreSiteUrlsList) {
      return moreSiteUrlsList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default()(moreSiteUrlsList).call(moreSiteUrlsList, function (siteUrl) {
        return _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_5___default()(siteUrl).call(siteUrl);
      }).join(';');
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11___default()(_this), "handleTrimObject", function (objectOriginal) {
      var _context;

      return _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_4___default()(_context = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3___default()(objectOriginal)).call(_context, function (accumulator, key) {
        var item = objectOriginal[key];

        if (typeof item === 'string') {
          item = _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_5___default()(item).call(item);
        }

        accumulator[key] = item;
        return accumulator;
      }, {});
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11___default()(_this), "handleSubmit",
    /*#__PURE__*/
    _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()(
    /*#__PURE__*/
    _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee() {
      var _this$props, affName, userAccountInfo, moreSiteUrlsList, userAccountInfoAfterTrim, resultValidateUserAccount, moreSiteUrls, newUserAccountInfo, result;

      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context2) {
        while (1) {
          switch (_context2.prev = _context2.next) {
            case 0:
              _this$props = _this.props, affName = _this$props.affName, userAccountInfo = _this$props.userAccountInfo, moreSiteUrlsList = _this$props.moreSiteUrlsList;
              userAccountInfoAfterTrim = _this.handleTrimObject(userAccountInfo);
              resultValidateUserAccount = Object(_modules_validation__WEBPACK_IMPORTED_MODULE_19__["validateUserAccount"])(userAccountInfoAfterTrim, moreSiteUrlsList);
              window.scrollTo({
                top: 0,
                behavior: "smooth"
              });

              if (!resultValidateUserAccount.isValid) {
                _context2.next = 13;
                break;
              }

              moreSiteUrls = _this.handleGetMoreSiteUrls(moreSiteUrlsList);
              newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, userAccountInfoAfterTrim, {
                MoreSiteUrls: moreSiteUrls
              });
              _context2.next = 9;
              return _this.props.updateUserAccount(newUserAccountInfo);

            case 9:
              result = _context2.sent;

              if (result) {
                _this.setState({
                  isShowMessage: true,
                  isSuccess: result.IsSuccessful,
                  message: result.IsSuccessful ? "".concat(userAccountInfo.AffName, " has been updated successfully!") : result.Message
                }, function () {
                  return window.setTimeout(function () {
                    _this.hideMessage();

                    result.IsSuccessful && _this.props.getAffiliateInfo(affName, _modules_constants__WEBPACK_IMPORTED_MODULE_20__["userPanelMenuKey"].edit);
                  }, 3000);
                });
              } else {
                _this.setState({
                  isSuccess: false,
                  isShowMessage: true,
                  message: 'Update is failed!'
                }, function () {
                  return window.setTimeout(_this.hideMessage, 3000);
                });
              }

              _context2.next = 14;
              break;

            case 13:
              _this.setState({
                isSuccess: false,
                isShowMessage: true,
                message: resultValidateUserAccount.message
              }, function () {
                return window.setTimeout(_this.hideMessage, 3000);
              });

            case 14:
            case "end":
              return _context2.stop();
          }
        }
      }, _callee);
    })));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_13___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_11___default()(_this), "hideMessage", function () {
      _this.setState({
        isShowMessage: false
      });
    });

    _this.state = {
      isSuccess: true,
      isShowMessage: false,
      message: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_8___default()(Content, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      var affId = this.props.affId;
      affId && this.props.getUserAccountInfo(affId);
    }
  }, {
    key: "render",
    value: function render() {
      var _this$state = this.state,
          isSuccess = _this$state.isSuccess,
          isShowMessage = _this$state.isShowMessage,
          message = _this$state.message;
      return this.props.userAccountInfo && react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Container"], {
        fluid: true,
        className: "user-account-container"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message
        }
      })), react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Row"], null, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        xs: "6",
        className: "pl-0"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_Section__WEBPACK_IMPORTED_MODULE_21__["default"], {
        title: "Personal Information"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_sections_Personal__WEBPACK_IMPORTED_MODULE_22__["default"], null))), react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        xs: "6",
        className: "pr-0"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_Section__WEBPACK_IMPORTED_MODULE_21__["default"], {
        title: "Additional Information"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_sections_Additional__WEBPACK_IMPORTED_MODULE_23__["default"], null)))), react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Row"], null, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        xs: "12",
        className: "px-0"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_Section__WEBPACK_IMPORTED_MODULE_21__["default"], {
        title: "Email Format Preference"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_sections_EmailFormat__WEBPACK_IMPORTED_MODULE_24__["default"], null)))), react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Row"], null, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        xs: "12",
        className: "px-0"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_Section__WEBPACK_IMPORTED_MODULE_21__["default"], {
        title: "Website Information"
      }, this.props.moreSiteUrlsList && react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_sections_WebSite__WEBPACK_IMPORTED_MODULE_25__["default"], null)))), react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Row"], null, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Col"], {
        xs: "12",
        className: "px-0"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_Section__WEBPACK_IMPORTED_MODULE_21__["default"], {
        title: "Payment Information"
      }, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(_sections_Payment__WEBPACK_IMPORTED_MODULE_26__["default"], null)))), react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Row"], null, react__WEBPACK_IMPORTED_MODULE_14___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_17__["Button"], {
        className: "mb-3",
        color: "success",
        onClick: this.handleSubmit
      }, "Submit")));
    }
  }]);

  return Content;
}(react__WEBPACK_IMPORTED_MODULE_14__["PureComponent"]);

function mapStateToProps(_ref2) {
  var affCommonInfo = _ref2.affCommonInfo,
      edit = _ref2.edit;
  var affId = affCommonInfo.affId,
      affName = affCommonInfo.affName;
  var userAccountInfo = edit.userAccountInfo,
      moreSiteUrlsList = edit.moreSiteUrlsList;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    affId: affId,
    affName: affName,
    userAccountInfo: userAccountInfo,
    moreSiteUrlsList: moreSiteUrlsList
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_16__["bindActionCreators"])({
    getUserAccountInfo: _actions__WEBPACK_IMPORTED_MODULE_18__["getUserAccountInfo"],
    updateUserAccount: _actions__WEBPACK_IMPORTED_MODULE_18__["updateUserAccount"],
    getAffiliateInfo: _actions__WEBPACK_IMPORTED_MODULE_18__["getAffiliateInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_15__["connect"])(mapStateToProps, mapDispatchToProps)(Content));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/Section.jsx":
/*!*****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/Section.jsx ***!
  \*****************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _babel_runtime_corejs3_helpers_objectWithoutProperties__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/objectWithoutProperties */ "./node_modules/@babel/runtime-corejs3/helpers/objectWithoutProperties.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_objectWithoutProperties__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_objectWithoutProperties__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);



var Section = function Section(props) {
  var title = props.title,
      children = props.children,
      rest = _babel_runtime_corejs3_helpers_objectWithoutProperties__WEBPACK_IMPORTED_MODULE_0___default()(props, ["title", "children"]);

  return react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("fieldset", rest, react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("legend", null, react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("h6", null, title)), children);
};

/* harmony default export */ __webpack_exports__["default"] = (react__WEBPACK_IMPORTED_MODULE_1___default.a.memo(Section));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Additional.jsx":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Additional.jsx ***!
  \*****************************************************************************************************/
/*! exports provided: Additional, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Additional", function() { return Additional; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__);
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_service__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../modules/service */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/service.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../actions */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js");

















var Additional =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Additional, _PureComponent);

  function Additional(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default()(this, Additional);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(Additional).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChange", function (event) {
      var target = event.target;
      var name = Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["capitalizeFirstChar"])(target.name);

      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.props.userAccountInfo, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, name, target.value));

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default()(Additional, [{
    key: "_renderCountry",
    value: function _renderCountry(country) {
      var _context;

      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        for: "country",
        sm: 3,
        xl: 2,
        className: "required"
      }, "Country"), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 9,
        xl: 10
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "select",
        id: "country",
        name: "country",
        value: country,
        onChange: this.handleChange
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("option", {
        value: ""
      }, "Select One"), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default()(_context = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0___default()(_modules_constants__WEBPACK_IMPORTED_MODULE_15__["countriesList"])).call(_context, function (key) {
        return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement("option", {
          key: 'country-' + key,
          value: key
        }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["countriesList"][key]);
      }))));
    }
  }, {
    key: "render",
    value: function render() {
      var userAccountInfo = this.props.userAccountInfo;
      return userAccountInfo && react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Form"], {
        tag: "div"
      }, Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('address', 'Address', userAccountInfo.Address, false, true, this.handleChange), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('city', 'City', userAccountInfo.City, false, true, this.handleChange), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('state', 'State', userAccountInfo.State, false, false, this.handleChange), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('postcode', 'Postcode/Zip', userAccountInfo.Postcode, false, true, this.handleChange), this._renderCountry(userAccountInfo.Country), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('question', 'Security Question', userAccountInfo.Question, true), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('answer', 'Answer', userAccountInfo.Answer, true));
    }
  }]);

  return Additional;
}(react__WEBPACK_IMPORTED_MODULE_10__["PureComponent"]);

function mapStateToProps(_ref) {
  var edit = _ref.edit;
  var userAccountInfo = edit.userAccountInfo;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
    userAccountInfo: userAccountInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_12__["bindActionCreators"])({
    setUserAccountInfo: _actions__WEBPACK_IMPORTED_MODULE_16__["setUserAccountInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_11__["connect"])(mapStateToProps, mapDispatchToProps)(Additional));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/EmailFormat.jsx":
/*!******************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/EmailFormat.jsx ***!
  \******************************************************************************************************/
/*! exports provided: EmailFormat, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EmailFormat", function() { return EmailFormat; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../../actions */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js");













var EmailFormat =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_6___default()(EmailFormat, _PureComponent);

  function EmailFormat(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default()(this, EmailFormat);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_3___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_4___default()(EmailFormat).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_7___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_5___default()(_this), "handleChange", function (isTextFormat) {
      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, _this.props.userAccountInfo, {
        MailOption: isTextFormat
      });

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default()(EmailFormat, [{
    key: "render",
    value: function render() {
      var _this2 = this;

      var userAccountInfo = this.props.userAccountInfo;
      var isTextFormat = !!userAccountInfo && userAccountInfo.MailOption;
      return userAccountInfo && react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["Form"], {
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["Col"], {
        sm: 5
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["FormGroup"], {
        check: true,
        className: "d-inline-block mr-3"
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["Label"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["Input"], {
        type: "radio",
        checked: isTextFormat,
        onChange: function onChange() {
          return _this2.handleChange(true);
        }
      }), ' ', " Plain Text Format")), react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["FormGroup"], {
        check: true,
        className: "d-inline-block"
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["Label"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_8___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_11__["Input"], {
        type: "radio",
        checked: !isTextFormat,
        onChange: function onChange() {
          return _this2.handleChange(false);
        }
      }), ' ', " Html Format")))));
    }
  }]);

  return EmailFormat;
}(react__WEBPACK_IMPORTED_MODULE_8__["PureComponent"]);

function mapStateToProps(_ref) {
  var edit = _ref.edit;
  var userAccountInfo = edit.userAccountInfo;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    userAccountInfo: userAccountInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_10__["bindActionCreators"])({
    setUserAccountInfo: _actions__WEBPACK_IMPORTED_MODULE_12__["setUserAccountInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_9__["connect"])(mapStateToProps, mapDispatchToProps)(EmailFormat));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Payment.jsx":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Payment.jsx ***!
  \**************************************************************************************************/
/*! exports provided: Payment, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Payment", function() { return Payment; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_service__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../../modules/service */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/service.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../actions */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../modules/constants */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/constants.js");
















var paymentMethodList = [{
  key: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["playerMethod"],
  value: '12BET player account'
}, {
  key: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["netellerMethod"],
  value: 'Neteller'
}, {
  key: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["moneybookersMethod"],
  value: 'Moneybookers'
}, {
  key: _modules_constants__WEBPACK_IMPORTED_MODULE_15__["wireMethod"],
  value: 'Bank Transfer'
}];
var Payment =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(Payment, _PureComponent);

  function Payment(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_2___default()(this, Payment);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(Payment).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleChangePaymentMethod", function (event) {
      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, _this.props.userAccountInfo, {
        PreferredPayment: event.target.value
      });

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "handleChange", function (event) {
      var target = event.target;
      var name = Object(_modules_service__WEBPACK_IMPORTED_MODULE_13__["capitalizeFirstChar"])(target.name);

      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, _this.props.userAccountInfo, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()({}, name, target.value));

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_3___default()(Payment, [{
    key: "_renderFormByMethod",
    value: function _renderFormByMethod(userAccountInfo) {
      var _ref;

      return _ref = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_ref, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["playerMethod"], this._renderPaymentForm('playerUsername', '12BET User Name', userAccountInfo.PlayerUsername, true, this.handleChange)), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_ref, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["netellerMethod"], this._renderPaymentForm('netellerAccountId', 'Neteller Account ID', userAccountInfo.NetellerAccountId, true, this.handleChange)), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_ref, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["moneybookersMethod"], this._renderPaymentForm('moneybookersEmail', 'Moneybookers Email', userAccountInfo.MoneybookersEmail, true, this.handleChange)), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(_ref, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["wireMethod"], this._renderWireForm(userAccountInfo)), _ref;
    }
  }, {
    key: "_renderPaymentForm",
    value: function _renderPaymentForm(name, label, inputValue, isRequired, changeHandler) {
      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Label"], {
        for: name,
        sm: 3,
        xl: 2,
        className: isRequired ? "required" : ""
      }, label), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Col"], {
        sm: 5,
        xl: 6
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Input"], {
        type: "text",
        id: name,
        name: name,
        value: inputValue,
        onChange: changeHandler
      })));
    }
  }, {
    key: "_renderWireForm",
    value: function _renderWireForm(userAccountInfo) {
      return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_9___default.a.Fragment, null, this._renderPaymentForm('bankName', 'Bank Name', userAccountInfo.BankName, true, this.handleChange), this._renderPaymentForm('bankAddress', 'Bank Address', userAccountInfo.BankAddress, true, this.handleChange), this._renderPaymentForm('bankCode', 'Bank ABA/Swift/Sort Code', userAccountInfo.BankCode, false, this.handleChange), this._renderPaymentForm('bankAccountName', 'Bank Account Name', userAccountInfo.BankAccountName, true, this.handleChange), this._renderPaymentForm('bankAccountNumber', 'Bank Account Number', userAccountInfo.BankAccountNumber, true, this.handleChange));
    }
  }, {
    key: "render",
    value: function render() {
      var userAccountInfo = this.props.userAccountInfo;

      var renderFormByMethod = userAccountInfo && this._renderFormByMethod(userAccountInfo);

      return userAccountInfo && react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Form"], {
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Label"], {
        for: "preferredPayment",
        sm: 3,
        xl: 2
      }, "Preferred Payment Method"), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Col"], {
        sm: 5,
        xl: 6
      }, react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_12__["Input"], {
        type: "select",
        id: "preferredPayment",
        name: "preferredPayment",
        onChange: this.handleChangePaymentMethod,
        value: userAccountInfo.PreferredPayment
      }, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(paymentMethodList).call(paymentMethodList, function (item) {
        return react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("option", {
          key: item.key,
          value: item.key
        }, item.value);
      }))), react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("p", null, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["minimumPayment"])), renderFormByMethod[userAccountInfo.PreferredPayment], userAccountInfo.PreferredPayment === _modules_constants__WEBPACK_IMPORTED_MODULE_15__["playerMethod"] && react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("p", {
        className: "text-danger offset-sm-3 offset-xl-2 pl-1"
      }, _modules_constants__WEBPACK_IMPORTED_MODULE_15__["nondeposited"]));
    }
  }]);

  return Payment;
}(react__WEBPACK_IMPORTED_MODULE_9__["PureComponent"]);

function mapStateToProps(_ref2) {
  var edit = _ref2.edit;
  var userAccountInfo = edit.userAccountInfo;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, {
    userAccountInfo: userAccountInfo
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_11__["bindActionCreators"])({
    setUserAccountInfo: _actions__WEBPACK_IMPORTED_MODULE_14__["setUserAccountInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_10__["connect"])(mapStateToProps, mapDispatchToProps)(Payment));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Personal.jsx":
/*!***************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/Personal.jsx ***!
  \***************************************************************************************************/
/*! exports provided: Personal, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Personal", function() { return Personal; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_find_index__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/find-index */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/find-index.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_find_index__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_find_index__WEBPACK_IMPORTED_MODULE_1__);
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
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_service__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../modules/service */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/service.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../../actions */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js");
/* harmony import */ var _modules_dataHandler__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../../../../modules/dataHandler */ "./Areas/Portal/www/source/js/modules/dataHandler.js");

















var Personal =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(Personal, _PureComponent);

  function Personal(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_3___default()(this, Personal);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_5___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_6___default()(Personal).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChange", function (event) {
      var target = event.target;
      var name = Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["capitalizeFirstChar"])(target.name);

      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.props.userAccountInfo, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, name, target.value));

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChangeGender", function (isMale) {
      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.props.userAccountInfo, {
        Gender: isMale
      });

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "handleChangeContactApps", function (event) {
      var _context;

      var _event$target = event.target,
          name = _event$target.name,
          value = _event$target.value;

      var indexItem = _babel_runtime_corejs3_core_js_stable_instance_find_index__WEBPACK_IMPORTED_MODULE_1___default()(_context = _this.arrayContactApps).call(_context, function (item) {
        return item.Name === name;
      });

      var updateItem = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.arrayContactApps[indexItem], {
        Value: value
      });

      _this.arrayContactApps = Object(_modules_dataHandler__WEBPACK_IMPORTED_MODULE_16__["updateObjectInArray"])(_this.arrayContactApps, indexItem, updateItem);

      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, _this.props.userAccountInfo, {
        ContactAppList: _this.arrayContactApps
      });

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    var userAccountInfo = _this.props.userAccountInfo;
    _this.arrayContactApps = userAccountInfo && userAccountInfo.ContactAppList;
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_4___default()(Personal, [{
    key: "_renderGenderForm",
    value: function _renderGenderForm(isMale) {
      var _this2 = this;

      return react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        for: name,
        sm: 3,
        xl: 2,
        className: "required"
      }, "Gender"), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Col"], {
        sm: 9,
        xl: 10
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        check: true,
        className: "d-inline-block mr-3"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "radio",
        checked: isMale != null && isMale,
        onChange: function onChange() {
          return _this2.handleChangeGender(true);
        }
      }), ' ', " Male")), react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["FormGroup"], {
        check: true,
        className: "d-inline-block"
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Label"], {
        check: true
      }, react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Input"], {
        type: "radio",
        checked: isMale != null && !isMale,
        onChange: function onChange() {
          return _this2.handleChangeGender(false);
        }
      }), ' ', " Female"))));
    }
  }, {
    key: "render",
    value: function render() {
      var _context2,
          _this3 = this;

      var userAccountInfo = this.props.userAccountInfo;
      return userAccountInfo && react__WEBPACK_IMPORTED_MODULE_10___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Form"], {
        tag: "div"
      }, Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('username', 'User Name', userAccountInfo.AffName, true), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('firstName', 'First Name', userAccountInfo.FirstName, false, true, this.handleChange), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('lastName', 'Last Name', userAccountInfo.LastName, false, true, this.handleChange), this._renderGenderForm(userAccountInfo.Gender), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('email', 'Email Address', userAccountInfo.Email, false, true, this.handleChange), Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])('phone', 'Contact Number', userAccountInfo.Phone, false, true, this.handleChange), this.arrayContactApps && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(_context2 = this.arrayContactApps).call(_context2, function (contactApp) {
        return Object(_modules_service__WEBPACK_IMPORTED_MODULE_14__["renderInputForm"])(contactApp.Name, contactApp.Name, contactApp.Value, false, false, _this3.handleChangeContactApps);
      }));
    }
  }]);

  return Personal;
}(react__WEBPACK_IMPORTED_MODULE_10__["PureComponent"]);

function mapStateToProps(_ref) {
  var edit = _ref.edit;
  var userAccountInfo = edit.userAccountInfo;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, {
    userAccountInfo: userAccountInfo
  });
}

function mapDispatchToProp(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_12__["bindActionCreators"])({
    setUserAccountInfo: _actions__WEBPACK_IMPORTED_MODULE_15__["setUserAccountInfo"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_11__["connect"])(mapStateToProps, mapDispatchToProp)(Personal));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/WebSite.jsx":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/components/sections/WebSite.jsx ***!
  \**************************************************************************************************/
/*! exports provided: WebSite, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "WebSite", function() { return WebSite; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_splice__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/splice */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/splice.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_splice__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_splice__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/index-of */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/index-of.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/trim */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/trim.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4__);
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
/* harmony import */ var _modules_service__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../../modules/service */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/service.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ../../actions */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/actions/index.js");


















var WebSite =
/*#__PURE__*/
function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_10___default()(WebSite, _PureComponent);

  function WebSite(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default()(this, WebSite);

    _this = _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_7___default()(this, _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_8___default()(WebSite).call(this, props));

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9___default()(_this), "handleChange", function (event) {
      var target = event.target;
      var name = Object(_modules_service__WEBPACK_IMPORTED_MODULE_16__["capitalizeFirstChar"])(target.name);

      var newUserAccountInfo = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default()({}, _this.props.userAccountInfo, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()({}, name, target.value));

      _this.props.setUserAccountInfo(newUserAccountInfo);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9___default()(_this), "handleChangeMoreUrl", function (event) {
      var moreSiteUrlsList = _this.props.moreSiteUrlsList;
      var _event$target = event.target,
          name = _event$target.name,
          value = _event$target.value;
      moreSiteUrlsList[name] = value;

      _this.props.setMoreSiteUrlsList(moreSiteUrlsList);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9___default()(_this), "handleAddUrl", function () {
      var moreSiteUrlsList = _this.props.moreSiteUrlsList;

      var newMoreSiteUrlsList = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default()(moreSiteUrlsList).call(moreSiteUrlsList, function (siteUrl) {
        return _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2___default()(siteUrl).call(siteUrl);
      });

      var findIndex = _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1___default()(newMoreSiteUrlsList).call(newMoreSiteUrlsList, '');

      if (findIndex === -1) {
        _this.moreSiteUrlRefs.push(react__WEBPACK_IMPORTED_MODULE_12___default.a.createRef());

        moreSiteUrlsList.push('');

        _this.props.setMoreSiteUrlsList(moreSiteUrlsList);
      } else {
        window.alert("Please input Website URL ".concat(findIndex + 2));

        _this.moreSiteUrlRefs[findIndex].current.focus();
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_9___default()(_this), "handleDeleteUrl", function (siteIndex) {
      var _context;

      var moreSiteUrlsList = _this.props.moreSiteUrlsList;

      _babel_runtime_corejs3_core_js_stable_instance_splice__WEBPACK_IMPORTED_MODULE_0___default()(moreSiteUrlsList).call(moreSiteUrlsList, siteIndex, 1);

      _babel_runtime_corejs3_core_js_stable_instance_splice__WEBPACK_IMPORTED_MODULE_0___default()(_context = _this.moreSiteUrlRefs).call(_context, siteIndex, 1);

      _this.props.setMoreSiteUrlsList(moreSiteUrlsList);
    });

    _this.moreSiteUrlRefs = [];

    for (var i = 0; i < _this.props.moreSiteUrlsList.length; i++) {
      _this.moreSiteUrlRefs.push(react__WEBPACK_IMPORTED_MODULE_12___default.a.createRef());
    }

    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default()(WebSite, [{
    key: "_renderMoreSiteUrl",
    value: function _renderMoreSiteUrl(moreSiteUrlsList) {
      var _this2 = this;

      return react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_12___default.a.Fragment, null, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default()(moreSiteUrlsList).call(moreSiteUrlsList, function (value, index) {
        return react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["FormGroup"], {
          row: true,
          key: "siteUrl".concat(index)
        }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Label"], {
          for: index,
          sm: 3,
          xl: 2,
          className: "required"
        }, "Website URL ", index + 2), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Col"], {
          sm: 5,
          xl: 6
        }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("input", {
          className: "form-control",
          name: index,
          value: value,
          onChange: _this2.handleChangeMoreUrl,
          ref: _this2.moreSiteUrlRefs[index]
        })), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
          outline: true,
          color: "danger",
          className: "mb-2",
          onClick: function onClick() {
            return _this2.handleDeleteUrl(index);
          }
        }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("span", {
          "aria-hidden": true
        }, "Del")));
      }));
    }
  }, {
    key: "render",
    value: function render() {
      var _this$props = this.props,
          userAccountInfo = _this$props.userAccountInfo,
          moreSiteUrlsList = _this$props.moreSiteUrlsList;
      return react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Form"], {
        tag: "div"
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Label"], {
        for: "siteUrl",
        sm: 3,
        xl: 2,
        className: "required"
      }, "Website URL"), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Col"], {
        sm: 5,
        xl: 6
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Input"], {
        type: "text",
        name: "siteUrl",
        value: userAccountInfo.SiteUrl,
        onChange: this.handleChange
      })), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Button"], {
        outline: true,
        color: "secondary",
        className: "mb-2",
        onClick: this.handleAddUrl
      }, "Add Url")), this._renderMoreSiteUrl(moreSiteUrlsList), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["FormGroup"], {
        row: true
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Label"], {
        for: "noSiteDescription",
        sm: 3,
        xl: 2
      }, "I do not have a website Please describe how you would like to promote 12Bet if you do not have the URL ."), react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Col"], {
        sm: 6,
        xl: 7
      }, react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_15__["Input"], {
        type: "textarea",
        style: {
          height: 70
        },
        id: "noSiteDescription",
        name: "noSiteDescription",
        value: userAccountInfo.NoSiteDescription,
        onChange: this.handleChange
      }))));
    }
  }]);

  return WebSite;
}(react__WEBPACK_IMPORTED_MODULE_12__["PureComponent"]);

function mapStateToProps(_ref) {
  var edit = _ref.edit;
  var userAccountInfo = edit.userAccountInfo,
      moreSiteUrlsList = edit.moreSiteUrlsList;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_4___default()({}, {
    userAccountInfo: userAccountInfo,
    moreSiteUrlsList: moreSiteUrlsList
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_14__["bindActionCreators"])({
    setUserAccountInfo: _actions__WEBPACK_IMPORTED_MODULE_17__["setUserAccountInfo"],
    setMoreSiteUrlsList: _actions__WEBPACK_IMPORTED_MODULE_17__["setMoreSiteUrlsList"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_13__["connect"])(mapStateToProps, mapDispatchToProps)(WebSite));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/constants.js":
/*!***************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/constants.js ***!
  \***************************************************************************************/
/*! exports provided: userPanelMenuKey, regexEmail, messageList, requiredFieldList, requiredBankTranferList, playerMethod, netellerMethod, moneybookersMethod, wireMethod, minimumPayment, nondeposited, countriesList */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "regexEmail", function() { return regexEmail; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "messageList", function() { return messageList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "requiredFieldList", function() { return requiredFieldList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "requiredBankTranferList", function() { return requiredBankTranferList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "playerMethod", function() { return playerMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "netellerMethod", function() { return netellerMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "moneybookersMethod", function() { return moneybookersMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "wireMethod", function() { return wireMethod; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "minimumPayment", function() { return minimumPayment; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "nondeposited", function() { return nondeposited; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "countriesList", function() { return countriesList; });
/* harmony import */ var _customer_common_modules_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../customer/common/modules/constants */ "./Areas/Portal/www/source/js/pages/customer/common/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "userPanelMenuKey", function() { return _customer_common_modules_constants__WEBPACK_IMPORTED_MODULE_0__["userPanelMenuKey"]; });



var regexEmail = '^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,4})+$';
var messageList = {
  FirstName: 'First Name is required',
  LastName: 'Last Name is required',
  Gender: 'Gender is empty, Please Check!',
  Email: 'Email Address is invalid',
  Phone: 'Contact Number is required',
  Address: 'Address is required',
  City: 'City is required',
  Postcode: 'Postcode/Zip is required',
  Country: 'Please select country',
  SiteUrl: 'Website URL is required',
  PlayerUsername: '12BET User Name is required',
  NetellerAccountId: 'Neteller Account ID is required',
  MoneybookersEmail: 'Moneybookers Email is required',
  BankName: 'Bank Name is required',
  BankAddress: 'Bank Address is required',
  BankAccountName: 'Bank Account Name is required',
  BankAccountNumber: 'Bank Account Number is required'
};
var requiredFieldList = ['FirstName', 'LastName', 'Phone', 'Address', 'City', 'Postcode', 'SiteUrl'];
var requiredBankTranferList = ['BankName', 'BankAddress', 'BankAccountName', 'BankAccountNumber'];
var playerMethod = 'Player';
var netellerMethod = 'Neteller';
var moneybookersMethod = 'Moneybookers';
var wireMethod = 'Wire';
var minimumPayment = 'Minimum payment amount is $100';
var nondeposited = 'Non-deposited account only';
var countriesList = {
  'Albania': 'Albania',
  'Algeria': 'Algeria',
  'Angola': 'Angola',
  'Anguilla': 'Anguilla',
  'Argentina': 'Argentina',
  'Armenia': 'Armenia',
  'Aruba': 'Aruba',
  'Australia': 'Australia',
  'Austria': 'Austria',
  'Azerbaijan': 'Azerbaijan',
  'Bahamas': 'Bahamas',
  'Bahrain': 'Bahrain',
  'Bangladesh': 'Bangladesh',
  'Barbados': 'Barbados',
  'Belarus': 'Belarus',
  'Belgium': 'Belgium',
  'Belize': 'Belize',
  'Benin': 'Benin',
  'Bermuda': 'Bermuda',
  'Bhutan': 'Bhutan',
  'Bolivia': 'Bolivia',
  'Bosnia and Herzegovina': 'Bosnia and Herzegovina',
  'Botswana': 'Botswana',
  'Brazil': 'Brazil',
  'Brunei': 'Brunei Darussalam',
  'Bulgaria': 'Bulgaria',
  'Burkina': 'Burkina Faso',
  'Burundi': 'Burundi',
  'Cambodia': 'Cambodia',
  'Cameroon': 'Cameroon',
  'Canada': 'Canada',
  'Cape Verde': 'Cape Verde',
  'Cayman Islands': 'Cayman Islands',
  'Central African Rep': 'Central African Rep',
  'Chad': 'Chad',
  'Chile': 'Chile',
  "China (People's Rep)": "China (People's Rep)",
  'Colombia': 'Colombia',
  'Congo(Rep)': 'Congo(Rep)',
  'Cook Islands': 'Cook Islands',
  'Costa Rica': 'Costa Rica',
  "Cote d'Ivoire (Rep)": "Cote d'Ivoire (Rep)",
  'Croatia': 'Croatia',
  'Cyprus': 'Cyprus',
  'Czech (Rep)': 'Czech (Rep)',
  'Dem Rep of Congo': 'Dem Rep of Congo',
  'Denmark': 'Denmark',
  'Dominica (Commonwealth of)': 'Dominica (Commonwealth of)',
  'Dominican Rep.': 'Dominican Rep.',
  'Ecuador': 'Ecuador',
  'Egypt': 'Egypt',
  'El Salvador': 'El Salvador',
  'Eritrea': 'Eritrea',
  'Estonia': 'Estonia',
  'Ethiopia': 'Ethiopia',
  'Fiji': 'Fiji',
  'Finland': 'Finland',
  'France': 'France',
  'French Polynesia': 'French Polynesia',
  'Gabon': 'Gabon',
  'Georgia': 'Georgia',
  'Germany': 'Germany',
  'Ghana': 'Ghana',
  'Gibraltar': 'Gibraltar',
  'Greece': 'Greece',
  'Grenada': 'Grenada',
  'Guatemala': 'Guatemala',
  'Guinea': 'Guinea',
  'Guyana': 'Guyana',
  'Haiti': 'Haiti',
  'Honduras': 'Honduras',
  'Hong Kong': 'Hong Kong',
  'Hungary': 'Hungary',
  'Iceland': 'Iceland',
  'India': 'India',
  'Indonesia': 'Indonesia',
  'Iran': 'Iran',
  'Iraq': 'Iraq',
  'Ireland': 'Ireland',
  'Israel': 'Israel',
  'Italy': 'Italy',
  'Jamaica': 'Jamaica',
  'Japan': 'Japan',
  'Jordan': 'Jordan',
  'Kenya': 'Kenya',
  'Korea': 'Korea (Rep)',
  'Kuwait': 'Kuwait',
  "Lao People's Dem Rep": "Lao People's Dem Rep",
  'Latvia': 'Latvia',
  'Lesotho': 'Lesotho',
  'Liechtenstein': 'Liechtenstein',
  'Lithuania': 'Lithuania',
  'Luxembourg': 'Luxembourg',
  'Macao': 'Macao',
  'Macedonia': 'Macedonia',
  'Madagascar': 'Madagascar',
  'Malawi': 'Malawi',
  'Malaysia': 'Malaysia',
  'Maldives': 'Maldives',
  'Mali': 'Mali',
  'Malta': 'Malta',
  'Mauritania': 'Mauritania',
  'Mauritius': 'Mauritius',
  'Mexico': 'Mexico',
  'Moldova': 'Moldova',
  'Mongolia': 'Mongolia',
  'Morocco': 'Morocco',
  'Myanmar': 'Myanmar',
  'Namibia': 'Namibia',
  'Nauru': 'Nauru',
  'Nepal': 'Nepal',
  'Netherlands': 'Netherlands',
  'New Caledonia': 'New Caledonia',
  'New Zealand': 'New Zealand',
  'Niger': 'Niger',
  'Nigeria': 'Nigeria',
  'Norway': 'Norway',
  'Oman': 'Oman',
  'Pakistan': 'Pakistan',
  'Panama': 'Panama',
  'Papua New Guinea': 'Papua New Guinea',
  'Paraguay': 'Paraguay',
  'Peru': 'Peru',
  'Philippines': 'Philippines',
  'Poland': 'Poland',
  'Portugal': 'Portugal',
  'Qatar': 'Qatar',
  'Romania': 'Romania',
  'Russian Federation': 'Russian Federation',
  'Rwanda': 'Rwanda',
  'Saint Christopher (St. Kitts) and Nevis': 'Saint Christopher (St. Kitts) and Nevis',
  'Saint Lucia': 'Saint Lucia',
  'Saint Vincent and the Grenadines': 'Saint Vincent and the Grenadines',
  'Sao Tome and Principe': 'Sao Tome and Principe',
  'Saudi Arabia': 'Saudi Arabia',
  'Senegal': 'Senegal',
  'Seychelles': 'Seychelles',
  'Sierra Leone': 'Sierra Leone',
  'Singapore': 'Singapore',
  'Slovakia': 'Slovakia',
  'Slovenia': 'Slovenia',
  'Solomon Islands': 'Solomon Islands',
  'Somalia': 'Somalia',
  'South Africa': 'South Africa',
  'Spain': 'Spain',
  'Sri Lanka': 'Sri Lanka',
  'Sudan': 'Sudan',
  'Suriname': 'Suriname',
  'Swaziland': 'Swaziland',
  'Sweden': 'Sweden',
  'Switzerland': 'Switzerland',
  'Taiwan': 'Taiwan',
  'Tanzania': 'Tanzania',
  'Thailand': 'Thailand',
  'Togo': 'Togo',
  'Trinidad and Tobago': 'Trinidad and Tobago',
  'Tunisia': 'Tunisia',
  'Turkey': 'Turkey',
  'Uganda': 'Uganda',
  'Ukraine': 'Ukraine',
  'United Arab Emirates': 'United Arab Emirates',
  'United Kingdom of Great Britain': 'United Kingdom of Great Britain',
  'United States of America': 'United States of America',
  'Uruguay': 'Uruguay',
  'Venezuela': 'Venezuela',
  'Viet Nam': 'Viet Nam',
  'Western Samoa': 'Western Samoa',
  'Yemen': 'Yemen',
  'Yugoslavia': 'Yugoslavia',
  'Zambia': 'Zambia',
  'Zimbabwe': 'Zimbabwe'
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/service.js":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/service.js ***!
  \*************************************************************************************/
/*! exports provided: renderInputForm, capitalizeFirstChar */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "renderInputForm", function() { return renderInputForm; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "capitalizeFirstChar", function() { return capitalizeFirstChar; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/slice */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/slice.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");



var renderInputForm = function renderInputForm(name, label, inputValue, isStatic, isRequired, changeHandler) {
  return react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["FormGroup"], {
    row: true
  }, react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["Label"], {
    for: name,
    sm: 3,
    xl: 2,
    className: isRequired ? "required" : ""
  }, label), react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["Col"], {
    sm: 9,
    xl: 10
  }, isStatic ? react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("p", {
    className: "form-control-plaintext"
  }, inputValue) : react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_2__["Input"], {
    type: "text",
    id: name,
    name: name,
    value: inputValue,
    onChange: changeHandler
  })));
};
var capitalizeFirstChar = function capitalizeFirstChar(string) {
  return string.charAt(0).toUpperCase() + _babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_0___default()(string).call(string, 1);
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/validation.js":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/validation.js ***!
  \****************************************************************************************/
/*! exports provided: validateUserAccount */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "validateUserAccount", function() { return validateUserAccount; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/reduce */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/index-of */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/index-of.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/trim */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/trim.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _constants__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./constants */ "./Areas/Portal/www/source/js/pages/user-panel/user-account/modules/constants.js");






var getMessage = function getMessage(fieldName, value) {
  return value === '' ? _constants__WEBPACK_IMPORTED_MODULE_4__["messageList"][fieldName] + "<br />" : '';
};

var validateMoreSiteUrls = function validateMoreSiteUrls(moreSiteUrlsList) {
  var moreSiteUrls = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default()(moreSiteUrlsList).call(moreSiteUrlsList, function (siteUrl) {
    return _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_2___default()(siteUrl).call(siteUrl);
  });

  var emptySiteUrlIndex = _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_1___default()(moreSiteUrls).call(moreSiteUrls, '');

  var hasEmptySiteUrl = emptySiteUrlIndex !== -1;
  var message = hasEmptySiteUrl ? "Website URL ".concat(emptySiteUrlIndex + 2, " is required<br />") : '';
  return {
    message: message,
    isValid: !hasEmptySiteUrl
  };
};

var validatePaymentMethod = function validatePaymentMethod(userAccountInfo) {
  var message = '';
  var isValid = true;

  switch (userAccountInfo.PreferredPayment) {
    case _constants__WEBPACK_IMPORTED_MODULE_4__["playerMethod"]:
      message = getMessage('PlayerUsername', userAccountInfo.PlayerUsername);
      isValid = userAccountInfo.PlayerUsername !== '';
      break;

    case _constants__WEBPACK_IMPORTED_MODULE_4__["netellerMethod"]:
      message = getMessage('NetellerAccountId', userAccountInfo.NetellerAccountId);
      isValid = userAccountInfo.NetellerAccountId !== '';
      break;

    case _constants__WEBPACK_IMPORTED_MODULE_4__["moneybookersMethod"]:
      message = getMessage('MoneybookersEmail', userAccountInfo.MoneybookersEmail);
      isValid = userAccountInfo.MoneybookersEmail !== '';
      break;

    case _constants__WEBPACK_IMPORTED_MODULE_4__["wireMethod"]:
      message = _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_0___default()(_constants__WEBPACK_IMPORTED_MODULE_4__["requiredBankTranferList"]).call(_constants__WEBPACK_IMPORTED_MODULE_4__["requiredBankTranferList"], function (accumulator, field) {
        return accumulator + getMessage(field, userAccountInfo[field]);
      }, '');
      isValid = userAccountInfo.BankName !== '' && userAccountInfo.BankAddress !== '' && userAccountInfo.BankAccountName !== '' && userAccountInfo.BankAccountNumber !== '';
      break;
  }

  return {
    message: message,
    isValid: isValid
  };
};

var validateUserAccount = function validateUserAccount(userAccountInfo, moreSiteUrlsList) {
  var message = '';
  var checkEmail = new RegExp(_constants__WEBPACK_IMPORTED_MODULE_4__["regexEmail"]);
  var isValidGender = userAccountInfo.Gender != null;
  var isValidEmail = checkEmail.test(userAccountInfo.Email);
  var country = _constants__WEBPACK_IMPORTED_MODULE_4__["countriesList"][userAccountInfo.Country] === undefined ? "" : userAccountInfo.Country;
  message = isValidGender ? message : message + _constants__WEBPACK_IMPORTED_MODULE_4__["messageList"].Gender + "<br />";
  message = isValidEmail ? message : message + _constants__WEBPACK_IMPORTED_MODULE_4__["messageList"].Email + "<br />";
  message += getMessage('Country', country);
  message += _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_0___default()(_constants__WEBPACK_IMPORTED_MODULE_4__["requiredFieldList"]).call(_constants__WEBPACK_IMPORTED_MODULE_4__["requiredFieldList"], function (accumulator, field) {
    return accumulator + getMessage(field, userAccountInfo[field]);
  }, '');
  var isValid = isValidGender && isValidEmail && userAccountInfo.FirstName !== '' && userAccountInfo.LastName !== '' && userAccountInfo.Phone !== '' && userAccountInfo.Address !== '' && userAccountInfo.City !== '' && userAccountInfo.Postcode !== '' && userAccountInfo.Country !== '' && userAccountInfo.SiteUrl !== '';
  var resultValidateMoreSiteUrls = moreSiteUrlsList ? validateMoreSiteUrls(moreSiteUrlsList) : {
    message: '',
    isValid: true
  };
  var resultValidatePaymentMethod = validatePaymentMethod(userAccountInfo);
  return {
    message: message + resultValidateMoreSiteUrls.message + resultValidatePaymentMethod.message,
    isValid: isValid && resultValidateMoreSiteUrls.isValid && resultValidatePaymentMethod.isValid
  };
};

/***/ })

}]);
//# sourceMappingURL=userAccount.chunk.js.map