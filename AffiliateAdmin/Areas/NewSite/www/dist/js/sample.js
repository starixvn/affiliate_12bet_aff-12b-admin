/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
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
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./source/js/pages/sample/components/index.jsx");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./node_modules/react-dom/index.js":
/*!**************************************************************************************************!*\
  !*** delegated ./node_modules/react-dom/index.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/react-dom/index.js");

/***/ }),

/***/ "./node_modules/react-redux/es/index.js":
/*!*******************************************************************************************************!*\
  !*** delegated ./node_modules/react-redux/es/index.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \*******************************************************************************************************/
/*! exports provided: Provider, createProvider, connectAdvanced, connect */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/react-redux/es/index.js");

/***/ }),

/***/ "./node_modules/react/index.js":
/*!**********************************************************************************************!*\
  !*** delegated ./node_modules/react/index.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/react/index.js");

/***/ }),

/***/ "./node_modules/redux-thunk/es/index.js":
/*!*******************************************************************************************************!*\
  !*** delegated ./node_modules/redux-thunk/es/index.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \*******************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/redux-thunk/es/index.js");

/***/ }),

/***/ "./node_modules/redux/es/redux.js":
/*!*************************************************************************************************!*\
  !*** delegated ./node_modules/redux/es/redux.js from dll-reference vendor_53facc8c81a5717418b5 ***!
  \*************************************************************************************************/
/*! exports provided: createStore, combineReducers, bindActionCreators, applyMiddleware, compose, __DO_NOT_USE__ActionTypes */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference vendor_53facc8c81a5717418b5 */ "dll-reference vendor_53facc8c81a5717418b5"))("./node_modules/redux/es/redux.js");

/***/ }),

/***/ "./source/js/actions/sampleAction.js":
/*!*******************************************!*\
  !*** ./source/js/actions/sampleAction.js ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.addSample = undefined;

var _types = __webpack_require__(/*! ./types */ "./source/js/actions/types.js");

var addSample = exports.addSample = function addSample(data) {
    return {
        type: _types.ADD_SAMPLE,
        data: data
    };
};

/***/ }),

/***/ "./source/js/actions/types.js":
/*!************************************!*\
  !*** ./source/js/actions/types.js ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
/* Sample */
var ADD_SAMPLE = exports.ADD_SAMPLE = "ADD_SAMPLE";
var GET_SAMPLE = exports.GET_SAMPLE = "GET_SAMPLE";

/***/ }),

/***/ "./source/js/layouts/AppLayout.jsx":
/*!*****************************************!*\
  !*** ./source/js/layouts/AppLayout.jsx ***!
  \*****************************************/
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

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var AppLayout = function (_Component) {
  _inherits(AppLayout, _Component);

  function AppLayout(props) {
    _classCallCheck(this, AppLayout);

    return _possibleConstructorReturn(this, (AppLayout.__proto__ || Object.getPrototypeOf(AppLayout)).call(this, props));
  }

  _createClass(AppLayout, [{
    key: 'render',
    value: function render() {
      return _react2.default.createElement(
        'div',
        { id: 'container', className: 'container main-container' },
        this.props.children
      );
    }
  }]);

  return AppLayout;
}(_react.Component);

exports.default = AppLayout;

/***/ }),

/***/ "./source/js/modules/createReducer.js":
/*!********************************************!*\
  !*** ./source/js/modules/createReducer.js ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
    value: true
});
exports.default = createReducer;
function createReducer(initialState, handlers) {
    return function reducer() {
        var state = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : initialState;
        var action = arguments[1];

        if (handlers.hasOwnProperty(action.type)) {
            return handlers[action.type](state, action);
        } else {
            return state;
        }
    };
}

/***/ }),

/***/ "./source/js/pages/sample/actions/filterAction.js":
/*!********************************************************!*\
  !*** ./source/js/pages/sample/actions/filterAction.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.toggleSample = undefined;

var _types = __webpack_require__(/*! ./types */ "./source/js/pages/sample/actions/types.js");

var toggleSample = exports.toggleSample = function toggleSample(data) {
  return {
    type: _types.TOGGLE_SAMPLE,
    data: data
  };
};

/***/ }),

/***/ "./source/js/pages/sample/actions/index.js":
/*!*************************************************!*\
  !*** ./source/js/pages/sample/actions/index.js ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

var _filterAction = __webpack_require__(/*! ./filterAction */ "./source/js/pages/sample/actions/filterAction.js");

var filterAction = _interopRequireWildcard(_filterAction);

var _sampleAction = __webpack_require__(/*! ../../../actions/sampleAction */ "./source/js/actions/sampleAction.js");

var sampleAction = _interopRequireWildcard(_sampleAction);

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } else { var newObj = {}; if (obj != null) { for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) newObj[key] = obj[key]; } } newObj.default = obj; return newObj; } }

exports.default = _extends({}, filterAction, sampleAction);

/***/ }),

/***/ "./source/js/pages/sample/actions/types.js":
/*!*************************************************!*\
  !*** ./source/js/pages/sample/actions/types.js ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
var TOGGLE_SAMPLE = exports.TOGGLE_SAMPLE = 'TOGGLE_SAMPLE';

/***/ }),

/***/ "./source/js/pages/sample/components/AppContainer.jsx":
/*!************************************************************!*\
  !*** ./source/js/pages/sample/components/AppContainer.jsx ***!
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

var _reactRedux = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");

var _AppLayout = __webpack_require__(/*! ../../../layouts/AppLayout */ "./source/js/layouts/AppLayout.jsx");

var _AppLayout2 = _interopRequireDefault(_AppLayout);

var _Filter = __webpack_require__(/*! ./Filter */ "./source/js/pages/sample/components/Filter.jsx");

var _Filter2 = _interopRequireDefault(_Filter);

var _Content = __webpack_require__(/*! ./Content */ "./source/js/pages/sample/components/Content.jsx");

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

/***/ "./source/js/pages/sample/components/Content.jsx":
/*!*******************************************************!*\
  !*** ./source/js/pages/sample/components/Content.jsx ***!
  \*******************************************************/
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

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../actions */ "./source/js/pages/sample/actions/index.js");

var _actions2 = _interopRequireDefault(_actions);

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
        {
          className: "content-container",
          style: { display: this.props.isChecked ? "block" : "none" }
        },
        _react2.default.createElement(
          "p",
          null,
          "Sample Data"
        ),
        _react2.default.createElement(
          "ul",
          { className: "sample-list" },
          this.props.sampleList.map(function (item, i) {
            return _react2.default.createElement(
              "li",
              { key: i },
              item
            );
          })
        )
      );
    }
  }]);

  return Content;
}(_react.Component);

function mapStateToProps(state) {
  return Object.assign({}, {
    isChecked: state.isChecked,
    sampleList: state.sampleList
  });
}

function mapDispatchToProps(dispatch) {
  var addSample = _actions2.default.addSample;

  return (0, _redux.bindActionCreators)({ addSample: addSample }, dispatch);
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(Content);

/***/ }),

/***/ "./source/js/pages/sample/components/Filter.jsx":
/*!******************************************************!*\
  !*** ./source/js/pages/sample/components/Filter.jsx ***!
  \******************************************************/
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

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _actions = __webpack_require__(/*! ../actions */ "./source/js/pages/sample/actions/index.js");

var _actions2 = _interopRequireDefault(_actions);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _toConsumableArray(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) { arr2[i] = arr[i]; } return arr2; } else { return Array.from(arr); } }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var Filter = function (_Component) {
  _inherits(Filter, _Component);

  function Filter(props) {
    _classCallCheck(this, Filter);

    var _this = _possibleConstructorReturn(this, (Filter.__proto__ || Object.getPrototypeOf(Filter)).call(this, props));

    _this.onCheckBoxChange = function () {
      var newState = !_this.props.isChecked;
      _this.props.toggleSample(newState);
    };

    _this.addSample = function () {
      var newSampleList = [].concat(_toConsumableArray(_this.props.sampleList), ["sample " + ++_this.props.sampleList.length]);
      //console.log(newSampleList);
      _this.props.addSample(newSampleList);
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
          "div",
          null,
          _react2.default.createElement(
            "label",
            null,
            _react2.default.createElement("input", {
              type: "checkbox",
              checked: this.props.isChecked,
              onChange: this.onCheckBoxChange
            }),
            "Toggle Samle"
          ),
          _react2.default.createElement(
            "button",
            { type: "button", onClick: this.addSample },
            "Add Sample"
          )
        )
      );
    }
  }]);

  return Filter;
}(_react.Component);

function mapDispatchToProps(dispatch) {
  return (0, _redux.bindActionCreators)(_actions2.default, dispatch);
}

function mapStateToProps(state) {
  return Object.assign({}, { isChecked: state.isChecked, sampleList: state.sampleList });
}

exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(Filter);

/***/ }),

/***/ "./source/js/pages/sample/components/index.jsx":
/*!*****************************************************!*\
  !*** ./source/js/pages/sample/components/index.jsx ***!
  \*****************************************************/
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

var _reducers = __webpack_require__(/*! ../reducers */ "./source/js/pages/sample/reducers/index.js");

var _reducers2 = _interopRequireDefault(_reducers);

var _AppContainer = __webpack_require__(/*! ./AppContainer */ "./source/js/pages/sample/components/AppContainer.jsx");

var _AppContainer2 = _interopRequireDefault(_AppContainer);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }

function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }

var defaultState = Object.assign({}, {
  sampleList: ["sample 1", "sample 2", "sample 3"],
  isChecked: false
});

function configureStore(initialState) {
  var enhancer = (0, _redux.compose)((0, _redux.applyMiddleware)(_reduxThunk2.default // lets us dispatch() functions
  ));

  return (0, _redux.createStore)(_reducers2.default, initialState, enhancer);
}

var store = configureStore(defaultState);

var App = function (_React$Component) {
  _inherits(App, _React$Component);

  function App(props) {
    _classCallCheck(this, App);

    return _possibleConstructorReturn(this, (App.__proto__ || Object.getPrototypeOf(App)).call(this, props));
  }

  _createClass(App, [{
    key: "render",
    value: function render() {
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


_reactDom2.default.render(_react2.default.createElement(App, null), document.getElementById("app-container"));

/***/ }),

/***/ "./source/js/pages/sample/reducers/filterReducer.js":
/*!**********************************************************!*\
  !*** ./source/js/pages/sample/reducers/filterReducer.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.isChecked = undefined;

var _createReducer2 = __webpack_require__(/*! ../../../modules/createReducer */ "./source/js/modules/createReducer.js");

var _createReducer3 = _interopRequireDefault(_createReducer2);

var _types = __webpack_require__(/*! ../actions/types */ "./source/js/pages/sample/actions/types.js");

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

var isChecked = exports.isChecked = (0, _createReducer3.default)({}, _defineProperty({}, _types.TOGGLE_SAMPLE, function (state, action) {
  return action.data;
}));

/***/ }),

/***/ "./source/js/pages/sample/reducers/index.js":
/*!**************************************************!*\
  !*** ./source/js/pages/sample/reducers/index.js ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});

var _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; };

var _redux = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");

var _sampleReducer = __webpack_require__(/*! ../../../reducers/sampleReducer */ "./source/js/reducers/sampleReducer.js");

var sampleReducer = _interopRequireWildcard(_sampleReducer);

var _filterReducer = __webpack_require__(/*! ./filterReducer */ "./source/js/pages/sample/reducers/filterReducer.js");

var filterReducer = _interopRequireWildcard(_filterReducer);

function _interopRequireWildcard(obj) { if (obj && obj.__esModule) { return obj; } else { var newObj = {}; if (obj != null) { for (var key in obj) { if (Object.prototype.hasOwnProperty.call(obj, key)) newObj[key] = obj[key]; } } newObj.default = obj; return newObj; } }

exports.default = (0, _redux.combineReducers)(_extends({}, sampleReducer, filterReducer));

/***/ }),

/***/ "./source/js/reducers/sampleReducer.js":
/*!*********************************************!*\
  !*** ./source/js/reducers/sampleReducer.js ***!
  \*********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.sampleList = undefined;

var _createReducer;

var _types = __webpack_require__(/*! ../actions/types */ "./source/js/actions/types.js");

var _createReducer2 = __webpack_require__(/*! ../modules/createReducer.js */ "./source/js/modules/createReducer.js");

var _createReducer3 = _interopRequireDefault(_createReducer2);

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

var sampleList = exports.sampleList = (0, _createReducer3.default)({}, (_createReducer = {}, _defineProperty(_createReducer, _types.GET_SAMPLE, function (state, action) {
  return action.data;
}), _defineProperty(_createReducer, _types.ADD_SAMPLE, function (state, action) {
  return action.data;
}), _createReducer));

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
//# sourceMappingURL=sample.js.map