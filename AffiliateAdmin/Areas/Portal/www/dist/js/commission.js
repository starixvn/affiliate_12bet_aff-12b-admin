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

var _Content = __webpack_require__(/*! ./Content */ "./source/js/pages/commission/components/Content.jsx");

var _Content2 = _interopRequireDefault(_Content);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
//import Filter from "./Filter";


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
                _react2.default.createElement(_Content2.default, null)
            );
        }
    }]);

    return AppContainer;
}(_react.Component);

exports.default = (0, _reactRedux.connect)()(AppContainer);

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

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Content = function (_Component) {
    _inherits(Content, _Component);

    function Content(props) {
        _classCallCheck(this, Content);

        return _possibleConstructorReturn(this, (Content.__proto__ || Object.getPrototypeOf(Content)).call(this, props));
    }

    _createClass(Content, [{
        key: "render",
        value: function render() {
            return _react2.default.createElement(
                "div",
                { className: "content-container" },
                _react2.default.createElement(
                    "table",
                    { className: "aff-table", cellspacing: "1", cellpadding: "3", width: "100%", style: { tableLayout: "fixed" } },
                    _react2.default.createElement(
                        "thead",
                        null,
                        _react2.default.createElement(
                            "tr",
                            null,
                            _react2.default.createElement("th", { rowSpan: "2" }),
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
                )
            );
        }
    }]);

    return Content;
}(_react.Component);

exports.default = (0, _reactRedux.connect)()(Content);

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

var _AppContainer = __webpack_require__(/*! ./components/AppContainer */ "./source/js/pages/commission/components/AppContainer.jsx");

var _AppContainer2 = _interopRequireDefault(_AppContainer);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

function configureStore(initialState) {
  var enhancer = (0, _redux.compose)((0, _redux.applyMiddleware)(_reduxThunk2.default // lets us dispatch() functions
  ));

  return (0, _redux.createStore)({}, initialState, enhancer);
}

var store = configureStore();
console.log(store);

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


_reactDom2.default.render(_react2.default.createElement(App, null), document.getElementById("rootApp"));

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