/******/ (function(modules) { // webpackBootstrap
/******/ 	// install a JSONP callback for chunk loading
/******/ 	function webpackJsonpCallback(data) {
/******/ 		var chunkIds = data[0];
/******/ 		var moreModules = data[1];
/******/ 		var executeModules = data[2];
/******/
/******/ 		// add "moreModules" to the modules object,
/******/ 		// then flag all "chunkIds" as loaded and fire callback
/******/ 		var moduleId, chunkId, i = 0, resolves = [];
/******/ 		for(;i < chunkIds.length; i++) {
/******/ 			chunkId = chunkIds[i];
/******/ 			if(installedChunks[chunkId]) {
/******/ 				resolves.push(installedChunks[chunkId][0]);
/******/ 			}
/******/ 			installedChunks[chunkId] = 0;
/******/ 		}
/******/ 		for(moduleId in moreModules) {
/******/ 			if(Object.prototype.hasOwnProperty.call(moreModules, moduleId)) {
/******/ 				modules[moduleId] = moreModules[moduleId];
/******/ 			}
/******/ 		}
/******/ 		if(parentJsonpFunction) parentJsonpFunction(data);
/******/
/******/ 		while(resolves.length) {
/******/ 			resolves.shift()();
/******/ 		}
/******/
/******/ 		// add entry modules from loaded chunk to deferred list
/******/ 		deferredModules.push.apply(deferredModules, executeModules || []);
/******/
/******/ 		// run deferred modules when all chunks ready
/******/ 		return checkDeferredModules();
/******/ 	};
/******/ 	function checkDeferredModules() {
/******/ 		var result;
/******/ 		for(var i = 0; i < deferredModules.length; i++) {
/******/ 			var deferredModule = deferredModules[i];
/******/ 			var fulfilled = true;
/******/ 			for(var j = 1; j < deferredModule.length; j++) {
/******/ 				var depId = deferredModule[j];
/******/ 				if(installedChunks[depId] !== 0) fulfilled = false;
/******/ 			}
/******/ 			if(fulfilled) {
/******/ 				deferredModules.splice(i--, 1);
/******/ 				result = __webpack_require__(__webpack_require__.s = deferredModule[0]);
/******/ 			}
/******/ 		}
/******/ 		return result;
/******/ 	}
/******/
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// object to store loaded and loading chunks
/******/ 	// undefined = chunk not loaded, null = chunk preloaded/prefetched
/******/ 	// Promise = chunk loading, 0 = chunk loaded
/******/ 	var installedChunks = {
/******/ 		"js/commission": 0
/******/ 	};
/******/
/******/ 	var deferredModules = [];
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "dist/";
/******/
/******/ 	var jsonpArray = window["webpackJsonp"] = window["webpackJsonp"] || [];
/******/ 	var oldJsonpFunction = jsonpArray.push.bind(jsonpArray);
/******/ 	jsonpArray.push = webpackJsonpCallback;
/******/ 	jsonpArray = jsonpArray.slice();
/******/ 	for(var i = 0; i < jsonpArray.length; i++) webpackJsonpCallback(jsonpArray[i]);
/******/ 	var parentJsonpFunction = oldJsonpFunction;
/******/
/******/
/******/ 	// add entry module to deferred list
/******/ 	deferredModules.push(["./source/js/pages/commission/index.jsx","common"]);
/******/ 	// run deferred modules when ready
/******/ 	return checkDeferredModules();
/******/ })
/************************************************************************/
/******/ ({

/***/ "./node_modules/invariant/browser.js":
/*!****************************************************************************************************!*\
  !*** delegated ./node_modules/invariant/browser.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \****************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/invariant/browser.js");

/***/ }),

/***/ "./node_modules/prop-types/index.js":
/*!***************************************************************************************************!*\
  !*** delegated ./node_modules/prop-types/index.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/prop-types/index.js");

/***/ }),

/***/ "./source/js/pages/commission/components/AppContainer.jsx":
/*!****************************************************************!*\
  !*** ./source/js/pages/commission/components/AppContainer.jsx ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _AppLayout = __webpack_require__(/*! ../../../layouts/AppLayout */ "./source/js/layouts/AppLayout.jsx");

var _AppLayout2 = _interopRequireDefault(_AppLayout);

var _Filter = __webpack_require__(/*! ./Filter */ "./source/js/pages/commission/components/Filter.jsx");

var _Filter2 = _interopRequireDefault(_Filter);

var _Content = __webpack_require__(/*! ./Content */ "./source/js/pages/commission/components/Content.jsx");

var _Content2 = _interopRequireDefault(_Content);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var AppContainer = function (_Component) {
    _inherits(AppContainer, _Component);

    function AppContainer(props) {
        _classCallCheck(this, AppContainer);

        return _possibleConstructorReturn(this, (AppContainer.__proto__ || Object.getPrototypeOf(AppContainer)).call(this, props));
    }

    _createClass(AppContainer, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                _AppLayout2.default,
                null,
                _react2.default.createElement(_Filter2.default, null),
                _react2.default.createElement(_Content2.default, null)
            );
        }
    }]);

    return AppContainer;
}(_react.Component);

exports.default = (0, _reactRedux.connect)()(AppContainer);

/***/ }),

/***/ "./source/js/pages/commission/components/CommGroupInfo.jsx":
/*!*****************************************************************!*\
  !*** ./source/js/pages/commission/components/CommGroupInfo.jsx ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var CommGroupInfo = function (_Component) {
    _inherits(CommGroupInfo, _Component);

    function CommGroupInfo(props) {
        _classCallCheck(this, CommGroupInfo);

        return _possibleConstructorReturn(this, (CommGroupInfo.__proto__ || Object.getPrototypeOf(CommGroupInfo)).call(this, props));
    }

    _createClass(CommGroupInfo, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "table",
                { style: { tableLayout: "fixed" } },
                _react2.default.createElement(
                    "tbody",
                    null,
                    _react2.default.createElement(
                        "tr",
                        { className: "row-form" },
                        _react2.default.createElement(
                            "td",
                            { width: "90" },
                            "Group Name: "
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            _react2.default.createElement("input", { type: "text" }),
                            " ",
                            _react2.default.createElement(
                                "span",
                                { className: "text-danger" },
                                "*"
                            )
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        { className: "row-form" },
                        _react2.default.createElement(
                            "td",
                            null,
                            "Description: "
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            _react2.default.createElement("input", { type: "text", className: "text-desc" })
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        { className: "row-form" },
                        _react2.default.createElement(
                            "td",
                            null,
                            "Active All: "
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            _react2.default.createElement("input", { type: "checkbox" })
                        )
                    )
                )
            );
        }
    }]);

    return CommGroupInfo;
}(_react.Component);

exports.default = CommGroupInfo;

/***/ }),

/***/ "./source/js/pages/commission/components/Content.jsx":
/*!***********************************************************!*\
  !*** ./source/js/pages/commission/components/Content.jsx ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _reactBootstrap = __webpack_require__(/*! react-bootstrap */ "./node_modules/react-bootstrap/es/index.js");

var _GridData = __webpack_require__(/*! ./GridData */ "./source/js/pages/commission/components/GridData.jsx");

var _GridData2 = _interopRequireDefault(_GridData);

var _PopupContent = __webpack_require__(/*! ./PopupContent */ "./source/js/pages/commission/components/PopupContent.jsx");

var _PopupContent2 = _interopRequireDefault(_PopupContent);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Content = function (_Component) {
    _inherits(Content, _Component);

    function Content(props) {
        _classCallCheck(this, Content);

        var _this = _possibleConstructorReturn(this, (Content.__proto__ || Object.getPrototypeOf(Content)).call(this, props));

        _this.handleAddNewGroup = function () {
            _this.setState({ showPopup: true });
        };

        _this.handleHide = function () {
            _this.setState({ showPopup: false });
        };

        _this.state = {
            showPopup: false
        };
        return _this;
    }

    _createClass(Content, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "div",
                { className: "data-content" },
                _react2.default.createElement(
                    "div",
                    null,
                    _react2.default.createElement(
                        "a",
                        { href: "javascript:;", onClick: this.handleAddNewGroup, className: "btn-add-comm" },
                        "Add New Group ",
                        _react2.default.createElement("span", { "class": "glyphicon glyphicon-triangle-left", "aria-hidden": "true" })
                    )
                ),
                _react2.default.createElement("div", { className: "clearfix" }),
                _react2.default.createElement(
                    "div",
                    { className: "content-container table-responsive" },
                    _react2.default.createElement(_GridData2.default, null)
                ),
                _react2.default.createElement(
                    _reactBootstrap.Modal,
                    { show: this.state.showPopup, onHide: this.handleHide, bsSize: "large", backdrop: "static" },
                    _react2.default.createElement(
                        _reactBootstrap.Modal.Header,
                        { closeButton: true },
                        _react2.default.createElement(
                            _reactBootstrap.Modal.Title,
                            null,
                            _react2.default.createElement("span", { className: "iconpopup" }),
                            "Add a Commission Group"
                        )
                    ),
                    _react2.default.createElement(
                        _reactBootstrap.Modal.Body,
                        null,
                        _react2.default.createElement(
                            "div",
                            { className: "modal-body-wrapper" },
                            _react2.default.createElement(_PopupContent2.default, null)
                        )
                    )
                )
            );
        }
    }]);

    return Content;
}(_react.Component);

exports.default = (0, _reactRedux.connect)()(Content);

/***/ }),

/***/ "./source/js/pages/commission/components/DeductionSetting.jsx":
/*!********************************************************************!*\
  !*** ./source/js/pages/commission/components/DeductionSetting.jsx ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var DeductionSetting = function (_Component) {
    _inherits(DeductionSetting, _Component);

    function DeductionSetting(props) {
        _classCallCheck(this, DeductionSetting);

        return _possibleConstructorReturn(this, (DeductionSetting.__proto__ || Object.getPrototypeOf(DeductionSetting)).call(this, props));
    }

    _createClass(DeductionSetting, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "fieldset",
                null,
                _react2.default.createElement(
                    "legend",
                    null,
                    _react2.default.createElement(
                        "h4",
                        { className: "text-bold" },
                        "Deductions"
                    )
                ),
                _react2.default.createElement(
                    "p",
                    { className: "text-bold" },
                    "Platform Fee %"
                ),
                _react2.default.createElement(
                    "ul",
                    { className: "platformfee-group" },
                    _react2.default.createElement(
                        "li",
                        { className: "inline" },
                        _react2.default.createElement(
                            "select",
                            { className: "select-product" },
                            _react2.default.createElement(
                                "option",
                                null,
                                "1"
                            )
                        ),
                        _react2.default.createElement(
                            "select",
                            { className: "select-percent" },
                            _react2.default.createElement(
                                "option",
                                null,
                                "0.17"
                            )
                        ),
                        _react2.default.createElement(
                            "span",
                            { className: "sign" },
                            "x"
                        ),
                        _react2.default.createElement(
                            "span",
                            { className: "unit-name" },
                            "|Gross Win Loss|"
                        ),
                        _react2.default.createElement(
                            "span",
                            null,
                            _react2.default.createElement(
                                "button",
                                { type: "button", className: "btn-action" },
                                "+"
                            )
                        )
                    ),
                    _react2.default.createElement(
                        "li",
                        { className: "inline" },
                        _react2.default.createElement(
                            "select",
                            { className: "select-product" },
                            _react2.default.createElement(
                                "option",
                                null,
                                "1"
                            )
                        ),
                        _react2.default.createElement(
                            "select",
                            { className: "select-percent" },
                            _react2.default.createElement(
                                "option",
                                null,
                                "0.17"
                            )
                        ),
                        _react2.default.createElement(
                            "span",
                            { className: "sign" },
                            "x"
                        ),
                        _react2.default.createElement(
                            "span",
                            { className: "unit-name" },
                            "|Gross Win Loss|"
                        ),
                        _react2.default.createElement(
                            "span",
                            null,
                            _react2.default.createElement(
                                "button",
                                { type: "button", className: "btn-action" },
                                "-"
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    "div",
                    { className: "inline" },
                    _react2.default.createElement(
                        "span",
                        { className: "label" },
                        "Operational Fee%"
                    ),
                    _react2.default.createElement(
                        "select",
                        { className: "select-percent" },
                        _react2.default.createElement(
                            "option",
                            null,
                            "0.07"
                        )
                    ),
                    _react2.default.createElement(
                        "span",
                        { className: "sign" },
                        "x"
                    ),
                    _react2.default.createElement(
                        "span",
                        { className: "unit-name" },
                        "|Gross Win Loss|"
                    )
                ),
                _react2.default.createElement(
                    "div",
                    { className: "inline" },
                    _react2.default.createElement(
                        "span",
                        { className: "label" },
                        "Banking Fee%"
                    ),
                    _react2.default.createElement(
                        "select",
                        { className: "select-percent" },
                        _react2.default.createElement(
                            "option",
                            null,
                            "0.02"
                        )
                    ),
                    _react2.default.createElement(
                        "span",
                        { className: "sign" },
                        "x"
                    ),
                    _react2.default.createElement(
                        "span",
                        { className: "unit-name" },
                        "(Deposit + Withdrawal)"
                    )
                ),
                _react2.default.createElement(
                    "div",
                    { className: "inline" },
                    _react2.default.createElement(
                        "span",
                        { className: "label" },
                        "Adjustment"
                    ),
                    _react2.default.createElement("input", { type: "checkbox", className: "checkbox" })
                )
            );
        }
    }]);

    return DeductionSetting;
}(_react.Component);

exports.default = DeductionSetting;

/***/ }),

/***/ "./source/js/pages/commission/components/Filter.jsx":
/*!**********************************************************!*\
  !*** ./source/js/pages/commission/components/Filter.jsx ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _reactBootstrap = __webpack_require__(/*! react-bootstrap */ "./node_modules/react-bootstrap/es/index.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Filter = function (_Component) {
    _inherits(Filter, _Component);

    function Filter(props) {
        _classCallCheck(this, Filter);

        var _this = _possibleConstructorReturn(this, (Filter.__proto__ || Object.getPrototypeOf(Filter)).call(this, props));

        _this.handleClick = function (event) {
            event.preventDefault();
            console.log("submit");
        };

        return _this;
    }

    _createClass(Filter, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "div",
                { className: "filter-container" },
                _react2.default.createElement(
                    _reactBootstrap.Form,
                    { inline: true, onSubmit: this.handleClick, style: { backgroundColor: 'transparent' } },
                    _react2.default.createElement(
                        _reactBootstrap.FormGroup,
                        null,
                        _react2.default.createElement(
                            _reactBootstrap.ControlLabel,
                            null,
                            "Group Name"
                        ),
                        ' ',
                        _react2.default.createElement(_reactBootstrap.FormControl, { type: "text", bsClass: "search-box" })
                    ),
                    ' ',
                    _react2.default.createElement(
                        _reactBootstrap.Button,
                        { type: "submit" },
                        _react2.default.createElement("span", { id: "divsearch" })
                    )
                )
            );
        }
    }]);

    return Filter;
}(_react.Component);

exports.default = (0, _reactRedux.connect)()(Filter);

/***/ }),

/***/ "./source/js/pages/commission/components/GridData.jsx":
/*!************************************************************!*\
  !*** ./source/js/pages/commission/components/GridData.jsx ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var GridData = function (_Component) {
    _inherits(GridData, _Component);

    function GridData(props) {
        _classCallCheck(this, GridData);

        return _possibleConstructorReturn(this, (GridData.__proto__ || Object.getPrototypeOf(GridData)).call(this, props));
    }

    _createClass(GridData, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "table",
                { className: "aff-table table table-bordered", cellSpacing: "1", cellPadding: "3", width: "100%", style: { tableLayout: "fixed" } },
                _react2.default.createElement(
                    "thead",
                    null,
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement("th", { rowSpan: "2", width: "3%" }),
                        _react2.default.createElement(
                            "th",
                            { rowSpan: "2" },
                            "Name"
                        ),
                        _react2.default.createElement(
                            "th",
                            { rowSpan: "2" },
                            "No.Aff"
                        ),
                        _react2.default.createElement(
                            "th",
                            { rowSpan: "2" },
                            "Description"
                        ),
                        _react2.default.createElement(
                            "th",
                            { rowSpan: "2" },
                            "Creation"
                        ),
                        _react2.default.createElement(
                            "th",
                            { colSpan: "11" },
                            "Policy"
                        ),
                        _react2.default.createElement(
                            "th",
                            { rowSpan: "2" },
                            "Action"
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement("th", null),
                        _react2.default.createElement(
                            "th",
                            null,
                            "Tier"
                        ),
                        _react2.default.createElement(
                            "th",
                            { title: "Minimum all members' winlost amount" },
                            "Min AM($)"
                        ),
                        _react2.default.createElement(
                            "th",
                            { title: "Maximum all members' winlost amount" },
                            "Max AM($)"
                        ),
                        _react2.default.createElement(
                            "th",
                            { title: "Minimum number of qualified members" },
                            "Min QM"
                        ),
                        _react2.default.createElement(
                            "th",
                            { title: "Maximum number of qualified members" },
                            "Max QM"
                        ),
                        _react2.default.createElement(
                            "th",
                            null,
                            "Comm Rate"
                        ),
                        _react2.default.createElement(
                            "th",
                            null,
                            "Operational Fee"
                        ),
                        _react2.default.createElement(
                            "th",
                            null,
                            "Banking Fee"
                        ),
                        _react2.default.createElement(
                            "th",
                            null,
                            "Adjustment"
                        ),
                        _react2.default.createElement(
                            "th",
                            null,
                            "Qm Turnover"
                        )
                    )
                ),
                _react2.default.createElement(
                    "tbody",
                    null,
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "1"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "Default"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "1,507"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "Default System Commission Group"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "pgmDung 12/25/2009 01:10:51"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "4" },
                            "Tier Setting"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "1"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "5"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0.00"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "0.07"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "0.02"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "Yes"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            "100.00"
                        ),
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "6" },
                            _react2.default.createElement(
                                "a",
                                { href: "javascript:;" },
                                _react2.default.createElement("span", { className: "icedit" })
                            ),
                            ' ',
                            _react2.default.createElement(
                                "a",
                                { href: "javascript:;" },
                                _react2.default.createElement("span", { className: "icdelete1" })
                            )
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement(
                            "td",
                            null,
                            "2"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "1"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "2,999"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "6"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "10"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0.20"
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement(
                            "td",
                            null,
                            "3"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "3,000"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "45,000"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "11"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "50"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0.30"
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement(
                            "td",
                            null,
                            "4"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "45,001"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "\u221E"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "51"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "\u221E"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0.40"
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement(
                            "td",
                            { rowSpan: "2" },
                            "Platform Fee"
                        ),
                        _react2.default.createElement(
                            "td",
                            { colSpan: "5" },
                            "Sportsbook, Number Game, Virtual Sports, Keno, ColossusBets, Fishing World, Cashout, Lottery, Exchange, Lb Keno"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0.17"
                        )
                    ),
                    _react2.default.createElement(
                        "tr",
                        null,
                        _react2.default.createElement(
                            "td",
                            { colSpan: "5" },
                            "Sportsbook, Number Game, Virtual Sports, Keno, ColossusBets, Fishing World, Cashout, Lottery, Exchange, Lb Keno"
                        ),
                        _react2.default.createElement(
                            "td",
                            null,
                            "0.12"
                        )
                    )
                )
            );
        }
    }]);

    return GridData;
}(_react.Component);

exports.default = GridData;

/***/ }),

/***/ "./source/js/pages/commission/components/PopupContent.jsx":
/*!****************************************************************!*\
  !*** ./source/js/pages/commission/components/PopupContent.jsx ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _DeductionSetting = __webpack_require__(/*! ./DeductionSetting */ "./source/js/pages/commission/components/DeductionSetting.jsx");

var _DeductionSetting2 = _interopRequireDefault(_DeductionSetting);

var _QmTurnOverSetting = __webpack_require__(/*! ./QmTurnOverSetting */ "./source/js/pages/commission/components/QmTurnOverSetting.jsx");

var _QmTurnOverSetting2 = _interopRequireDefault(_QmTurnOverSetting);

var _TierSetting = __webpack_require__(/*! ./TierSetting */ "./source/js/pages/commission/components/TierSetting.jsx");

var _TierSetting2 = _interopRequireDefault(_TierSetting);

var _CommGroupInfo = __webpack_require__(/*! ./CommGroupInfo */ "./source/js/pages/commission/components/CommGroupInfo.jsx");

var _CommGroupInfo2 = _interopRequireDefault(_CommGroupInfo);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var PopupContent = function (_Component) {
    _inherits(PopupContent, _Component);

    function PopupContent(props) {
        _classCallCheck(this, PopupContent);

        return _possibleConstructorReturn(this, (PopupContent.__proto__ || Object.getPrototypeOf(PopupContent)).call(this, props));
    }

    _createClass(PopupContent, [{
        key: 'render',
        value: function render() {
            return _react2.default.createElement(
                'div',
                { className: 'comm-popup-container' },
                _react2.default.createElement(_CommGroupInfo2.default, null),
                _react2.default.createElement(_DeductionSetting2.default, null),
                _react2.default.createElement(_QmTurnOverSetting2.default, null),
                _react2.default.createElement(_TierSetting2.default, null),
                _react2.default.createElement(
                    'button',
                    { type: 'button' },
                    'SAVE'
                )
            );
        }
    }]);

    return PopupContent;
}(_react.Component);

exports.default = PopupContent;

/***/ }),

/***/ "./source/js/pages/commission/components/QmTurnOverSetting.jsx":
/*!*********************************************************************!*\
  !*** ./source/js/pages/commission/components/QmTurnOverSetting.jsx ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var QmTurnOverSetting = function (_Component) {
    _inherits(QmTurnOverSetting, _Component);

    function QmTurnOverSetting(props) {
        _classCallCheck(this, QmTurnOverSetting);

        return _possibleConstructorReturn(this, (QmTurnOverSetting.__proto__ || Object.getPrototypeOf(QmTurnOverSetting)).call(this, props));
    }

    _createClass(QmTurnOverSetting, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "fieldset",
                null,
                _react2.default.createElement(
                    "legend",
                    null,
                    _react2.default.createElement(
                        "h4",
                        { className: "text-bold" },
                        "QM Turnover"
                    )
                ),
                _react2.default.createElement(
                    "p",
                    null,
                    "A member is considered a Qualified Member (QM) when his/her turnover in a month is equivalent to"
                ),
                _react2.default.createElement(
                    "p",
                    null,
                    "USD ",
                    _react2.default.createElement(
                        "span",
                        null,
                        _react2.default.createElement("input", { type: "text", value: 100 })
                    )
                )
            );
        }
    }]);

    return QmTurnOverSetting;
}(_react.Component);

exports.default = QmTurnOverSetting;

/***/ }),

/***/ "./source/js/pages/commission/components/TierSetting.jsx":
/*!***************************************************************!*\
  !*** ./source/js/pages/commission/components/TierSetting.jsx ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var TierSetting = function (_Component) {
    _inherits(TierSetting, _Component);

    function TierSetting(props) {
        _classCallCheck(this, TierSetting);

        return _possibleConstructorReturn(this, (TierSetting.__proto__ || Object.getPrototypeOf(TierSetting)).call(this, props));
    }

    _createClass(TierSetting, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "fieldset",
                null,
                _react2.default.createElement(
                    "legend",
                    null,
                    _react2.default.createElement(
                        "h4",
                        { className: "text-bold" },
                        "Tier Setting"
                    )
                ),
                _react2.default.createElement(
                    "table",
                    { className: "table table-bordered tier-setting-table", style: { tableLayout: "fixed" } },
                    _react2.default.createElement(
                        "thead",
                        null,
                        _react2.default.createElement(
                            "tr",
                            null,
                            _react2.default.createElement(
                                "th",
                                { width: "40" },
                                "Tier"
                            ),
                            _react2.default.createElement(
                                "th",
                                null,
                                "Min AM"
                            ),
                            _react2.default.createElement(
                                "th",
                                null,
                                "Max AM"
                            ),
                            _react2.default.createElement(
                                "th",
                                null,
                                "Min QM"
                            ),
                            _react2.default.createElement(
                                "th",
                                null,
                                "Max QM"
                            ),
                            _react2.default.createElement(
                                "th",
                                { width: "100" },
                                "Commission"
                            ),
                            _react2.default.createElement(
                                "th",
                                { width: "50" },
                                "Action"
                            )
                        )
                    ),
                    _react2.default.createElement(
                        "tbody",
                        null,
                        _react2.default.createElement(
                            "tr",
                            null,
                            _react2.default.createElement(
                                "td",
                                { className: "text-center" },
                                "1"
                            ),
                            _react2.default.createElement(
                                "td",
                                null,
                                _react2.default.createElement("input", { type: "text", className: "input-tier", value: "0" })
                            ),
                            _react2.default.createElement(
                                "td",
                                null,
                                _react2.default.createElement("input", { type: "text", className: "input-tier max", value: "0" }),
                                _react2.default.createElement("input", { className: "chk-infinity", type: "checkbox" }),
                                "\u221E"
                            ),
                            _react2.default.createElement(
                                "td",
                                null,
                                _react2.default.createElement("input", { type: "text", className: "input-tier", value: "0" })
                            ),
                            _react2.default.createElement(
                                "td",
                                null,
                                _react2.default.createElement("input", { type: "text", className: "input-tier max", value: "5" }),
                                _react2.default.createElement("input", { className: "chk-infinity", type: "checkbox", disabled: true }),
                                "\u221E"
                            ),
                            _react2.default.createElement(
                                "td",
                                null,
                                _react2.default.createElement(
                                    "select",
                                    { className: "select-comm" },
                                    _react2.default.createElement(
                                        "option",
                                        null,
                                        "0.00"
                                    )
                                )
                            ),
                            _react2.default.createElement(
                                "td",
                                { className: "text-center" },
                                _react2.default.createElement(
                                    "a",
                                    { href: "javascript:;" },
                                    _react2.default.createElement("span", { className: "icdelete1" })
                                )
                            )
                        )
                    )
                ),
                _react2.default.createElement(
                    "div",
                    null,
                    _react2.default.createElement(
                        "button",
                        { type: "button", className: "btn-addTier" },
                        "Add Tier"
                    )
                )
            );
        }
    }]);

    return TierSetting;
}(_react.Component);

exports.default = TierSetting;

/***/ }),

/***/ "./source/js/pages/commission/index.jsx":
/*!**********************************************!*\
  !*** ./source/js/pages/commission/index.jsx ***!
  \**********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

var _react = __webpack_require__(/*! react */ "./node_modules/react/index.js");

var _react2 = _interopRequireDefault(_react);

var _reactDom = __webpack_require__(/*! react-dom */ "./node_modules/react-dom/index.js");

var _reactDom2 = _interopRequireDefault(_reactDom);

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _reduxThunk = __webpack_require__(/*! redux-thunk */ "./node_modules/redux-thunk/es/index.js");

var _reduxThunk2 = _interopRequireDefault(_reduxThunk);

var _reducers = __webpack_require__(/*! ./reducers */ "./source/js/pages/commission/reducers/index.js");

var _reducers2 = _interopRequireDefault(_reducers);

var _AppContainer = __webpack_require__(/*! ./components/AppContainer */ "./source/js/pages/commission/components/AppContainer.jsx");

var _AppContainer2 = _interopRequireDefault(_AppContainer);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

function configureStore(initialState) {
  var enhancer = (0, _redux.compose)((0, _redux.applyMiddleware)(_reduxThunk2.default // lets us dispatch() functions
  ));

  return (0, _redux.createStore)(_reducers2.default, initialState, enhancer);
}

var store = configureStore({});

var App = function (_React$Component) {
  _inherits(App, _React$Component);

  function App(props) {
    _classCallCheck(this, App);

    return _possibleConstructorReturn(this, (App.__proto__ || Object.getPrototypeOf(App)).call(this, props));
  }

  _createClass(App, [{
    key: "render",
    value: function render() {
      console.log(store);
      return _react2.default.createElement(
        _reactRedux.Provider,
        { store: store },
        _react2.default.createElement(_AppContainer2.default, null)
      );
    }
  }]);

  return App;
}(_react2.default.Component);

exports.default = App;


_reactDom2.default.render(_react2.default.createElement(App, null), document.getElementById("root"));

/***/ }),

/***/ "./source/js/pages/commission/reducers/index.js":
/*!******************************************************!*\
  !*** ./source/js/pages/commission/reducers/index.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

exports.default = (0, _redux.combineReducers)({});

/***/ }),

/***/ "dll-reference vendor_53facc8c81a5717418b5":
/*!**********************************************!*\
  !*** external "vendor_53facc8c81a5717418b5" ***!
  \**********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = vendor_53facc8c81a5717418b5;

/***/ })

/******/ });
//# sourceMappingURL=commission.js.map