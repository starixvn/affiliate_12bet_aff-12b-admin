(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[0],{

/***/ "./Areas/Portal/www/source/js/actions/actionTypes.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/source/js/actions/actionTypes.js ***!
  \***********************************************************/
/*! exports provided: SET_PAGING_OPTION, TOGGLE_LOADER */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_PAGING_OPTION", function() { return SET_PAGING_OPTION; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TOGGLE_LOADER", function() { return TOGGLE_LOADER; });
var SET_PAGING_OPTION = 'SET_PAGING_OPTION';
var TOGGLE_LOADER = 'TOGGLE_LOADER';

/***/ }),

/***/ "./Areas/Portal/www/source/js/actions/loaderAction.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/actions/loaderAction.js ***!
  \************************************************************/
/*! exports provided: toggleLoader */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "toggleLoader", function() { return toggleLoader; });
/* harmony import */ var _actionTypes__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");

var toggleLoader = function toggleLoader(data) {
  return {
    type: _actionTypes__WEBPACK_IMPORTED_MODULE_0__["TOGGLE_LOADER"],
    data: data
  };
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/actions/pagingAction.js":
/*!************************************************************!*\
  !*** ./Areas/Portal/www/source/js/actions/pagingAction.js ***!
  \************************************************************/
/*! exports provided: setPagingOption */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setPagingOption", function() { return setPagingOption; });
/* harmony import */ var _actionTypes__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");

var setPagingOption = function setPagingOption(data) {
  return {
    type: _actionTypes__WEBPACK_IMPORTED_MODULE_0__["SET_PAGING_OPTION"],
    data: data
  };
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/Loader.jsx":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/Loader.jsx ***!
  \**********************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return Loader; });
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
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_9__);










function ownKeys(object, enumerableOnly) { var keys = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_7___default()(object); if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default.a) { var symbols = _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default()(object); if (enumerableOnly) symbols = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_5___default()(symbols).call(symbols, function (sym) { return _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { var _context; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context = ownKeys(Object(source), true)).call(_context, function (key) { _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_8___default()(target, key, source[key]); }); } else if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default.a) { _babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1___default()(target, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default()(source)); } else { var _context2; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context2 = ownKeys(Object(source))).call(_context2, function (key) { _babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0___default()(target, key, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(source, key)); }); } } return target; }


function Loader(props) {
  var backdrop = props.backdrop === undefined ? true : props.backdrop;
  var style = props.style;
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
    className: "loader",
    style: {
      display: props.show ? 'block' : 'none'
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
    className: "backdrop",
    style: {
      display: backdrop ? 'block' : 'none'
    }
  }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", {
    className: "spinner",
    style: _objectSpread({}, style)
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_9___default.a.createElement("div", null)));
}

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelect.jsx":
/*!*********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelect.jsx ***!
  \*********************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return MultipleSelect; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/filter */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/filter.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3__);
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
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(prop_types__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var _MultipleSelectLabel__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./MultipleSelectLabel */ "./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelectLabel.jsx");
/* harmony import */ var _MultipleSelectOptionList__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./MultipleSelectOptionList */ "./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelectOptionList.jsx");
/* harmony import */ var _OptionAll__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./OptionAll */ "./Areas/Portal/www/source/js/components/multiple-select/components/OptionAll.jsx");












function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }






var KEY_NAME = "key";
var VALUE_NAME = "value";
var STATUS_NAME = "checked";
var id = 1;

var MultipleSelect = /*#__PURE__*/function (_Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(MultipleSelect, _Component);

  var _super = _createSuper(MultipleSelect);

  function MultipleSelect(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, MultipleSelect);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "onChangeHandler", function (item) {
      var _selectedItem;

      var _this$props = _this.props,
          keyField = _this$props.keyField,
          valueField = _this$props.valueField,
          statusField = _this$props.statusField,
          dataSource = _this$props.dataSource;
      var selectedItem = (_selectedItem = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_selectedItem, keyField, item.key), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_selectedItem, valueField, item.value), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_selectedItem, statusField, item.checked), _selectedItem);

      var newDataSource = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default()(dataSource).call(dataSource, function (option) {
        var _ref;

        var newItem = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, option);

        if (newItem[keyField] === item.key) {
          newItem[statusField] = item.checked;
        }

        return _ref = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref, KEY_NAME, newItem[keyField]), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref, VALUE_NAME, newItem[valueField]), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref, STATUS_NAME, newItem[statusField]), _ref;
      });

      _this._callBackToParent(selectedItem, newDataSource);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "onChangeItemDefaultHandler", function (checked) {
      var _this$props2 = _this.props,
          statusField = _this$props2.statusField,
          itemDefaultOption = _this$props2.itemDefaultOption;

      var selectedItem = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_2___default()({}, itemDefaultOption, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, statusField, checked));

      _this.props.onChange && _this.props.onChange(selectedItem, '', true);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "onToggle", function () {
      _this.setState(function (prevState) {
        return {
          showOptionList: !prevState.showOptionList
        };
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "checkAllHandler", function (checked) {
      var _this$props3 = _this.props,
          keyField = _this$props3.keyField,
          valueField = _this$props3.valueField,
          dataSource = _this$props3.dataSource;

      var newDataSource = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default()(dataSource).call(dataSource, function (item) {
        return {
          key: item[keyField],
          value: item[valueField],
          checked: checked
        };
      });

      _this._callBackToParent(null, newDataSource);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_close", function () {
      _this.setState({
        showOptionList: false
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_handleDocumentClick", function (event) {
      var clickOutside = _this.wrapper && !_this.wrapper.contains(event.target);

      if (clickOutside && _this.state.showOptionList) {
        _this._close();
      }
    });

    _this.id = "multiple-select-".concat(props.id || id++);
    _this.state = {
      showOptionList: false
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(MultipleSelect, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      document.addEventListener('click', this._handleDocumentClick);
    }
  }, {
    key: "componentWillUnmount",
    value: function componentWillUnmount() {
      document.removeEventListener('click', this._handleDocumentClick);
    }
  }, {
    key: "_callBackToParent",
    value: function _callBackToParent(selectedItem, newDataSource) {
      var selectedItemsKey = this._getSelectedItemKey(newDataSource);

      this.props.onChange && this.props.onChange(selectedItem, selectedItemsKey);
    }
  }, {
    key: "_convertDataSource",
    value: function _convertDataSource() {
      var _this$props4 = this.props,
          keyField = _this$props4.keyField,
          valueField = _this$props4.valueField,
          statusField = _this$props4.statusField,
          dataSource = _this$props4.dataSource;
      return _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_3___default()(dataSource).call(dataSource, function (item) {
        var _ref2;

        return _ref2 = {}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref2, KEY_NAME, item[keyField]), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref2, VALUE_NAME, item[valueField]), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_ref2, STATUS_NAME, item[statusField]), _ref2;
      });
    }
  }, {
    key: "_getSelectedItemKey",
    value: function _getSelectedItemKey(dataSource) {
      var selectedItemKey = [];

      for (var i = 0; i < dataSource.length; i++) {
        var item = dataSource[i];

        if (item.checked) {
          selectedItemKey.push(item.key);
        }
      }

      return selectedItemKey.join(",");
    }
  }, {
    key: "_renderOptionAll",
    value: function _renderOptionAll(dataSource, itemDefaultOption) {
      if (!itemDefaultOption) {
        var checkedItemCount = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_1___default()(dataSource).call(dataSource, function (x) {
          return x.checked;
        }).length;

        var checkedAll = checkedItemCount === dataSource.length;
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_OptionAll__WEBPACK_IMPORTED_MODULE_15__["default"], {
          id: this.id,
          checked: checkedAll,
          label: this.props.optionAllLabel,
          onChange: this.checkAllHandler
        });
      } else {
        var _this$props5 = this.props,
            valueField = _this$props5.valueField,
            statusField = _this$props5.statusField;
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_OptionAll__WEBPACK_IMPORTED_MODULE_15__["default"], {
          id: this.id,
          checked: itemDefaultOption[statusField],
          label: itemDefaultOption[valueField],
          onChange: this.onChangeItemDefaultHandler
        });
      }
    }
  }, {
    key: "render",
    value: function render() {
      var _this2 = this;

      var _this$props6 = this.props,
          noneSelectedLabel = _this$props6.noneSelectedLabel,
          maxDisplayItemCount = _this$props6.maxDisplayItemCount,
          hasAllOption = _this$props6.hasAllOption,
          isDisableOption = _this$props6.isDisableOption,
          itemDefaultOption = _this$props6.itemDefaultOption;

      var dataSource = this._convertDataSource();

      var selectedItems = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_1___default()(dataSource).call(dataSource, function (item) {
        return item.checked;
      });

      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "multiple-select-container",
        id: this.id,
        ref: function ref(element) {
          return _this2.wrapper = element;
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_MultipleSelectLabel__WEBPACK_IMPORTED_MODULE_13__["default"], {
        selectedItems: selectedItems,
        onToggle: this.onToggle,
        noneSelectedLabel: noneSelectedLabel,
        maxDisplayItemCount: maxDisplayItemCount
      }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "multiple-select-default multiple-select-options-container",
        style: {
          display: this.state.showOptionList ? "block" : "none"
        }
      }, hasAllOption && this._renderOptionAll(dataSource, itemDefaultOption), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(_MultipleSelectOptionList__WEBPACK_IMPORTED_MODULE_14__["default"], {
        id: this.id,
        dataSource: dataSource,
        onChange: this.onChangeHandler,
        isDisableOption: isDisableOption
      })));
    }
  }]);

  return MultipleSelect;
}(react__WEBPACK_IMPORTED_MODULE_11__["Component"]);

_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(MultipleSelect, "propTypes", {
  dataSource: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.arrayOf(prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.object).isRequired,
  keyField: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.string,
  valueField: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.string,
  statusField: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.string,
  noneSelectedLabel: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.string,
  optionAllLabel: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.string,
  maxDisplayItemCount: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.number,
  onChange: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.func,
  hasAllOption: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.bool,
  isDisableOption: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.bool,
  itemDefaultOption: prop_types__WEBPACK_IMPORTED_MODULE_12___default.a.object
});

_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(MultipleSelect, "defaultProps", {
  keyField: KEY_NAME,
  valueField: VALUE_NAME,
  statusField: STATUS_NAME,
  hasAllOption: true,
  isDisableOption: false,
  itemDefaultOption: null
});



/***/ }),

/***/ "./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelectLabel.jsx":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelectLabel.jsx ***!
  \**************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(prop_types__WEBPACK_IMPORTED_MODULE_2__);



var NO_SELECTED_LABEL = "Select options";
var MAX_SELECTED_ITEM_FOR_DISPLAY = 3;

function getSelectedItemsString(props) {
  var selectedItems = props.selectedItems,
      noneSelectedLabel = props.noneSelectedLabel,
      maxDisplayItemCount = props.maxDisplayItemCount;

  var selectedValues = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(selectedItems).call(selectedItems, function (item) {
    return item.value;
  });

  var displayText = noneSelectedLabel;
  var selectedItemCount = selectedValues.length;

  if (selectedItemCount >= maxDisplayItemCount) {
    displayText = "".concat(selectedItemCount, " selected");
  } else if (selectedItemCount >= 1) {
    displayText = selectedValues.join(", ");
  }

  return displayText;
}

var MultipleSelectLabel = function MultipleSelectLabel(props) {
  var selectedItemString = getSelectedItemsString(props);
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("button", {
    type: "button",
    className: "multiple-select-default multiple-select-label",
    onClick: props.onToggle
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("span", {
    className: "text-display",
    title: selectedItemString
  }, selectedItemString), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("b", {
    className: "caret"
  }));
};

MultipleSelectLabel.propTypes = {
  selectedItems: prop_types__WEBPACK_IMPORTED_MODULE_2___default.a.arrayOf(prop_types__WEBPACK_IMPORTED_MODULE_2___default.a.object).isRequired,
  noneSelectedLabel: prop_types__WEBPACK_IMPORTED_MODULE_2___default.a.string,
  maxDisplayItemCount: prop_types__WEBPACK_IMPORTED_MODULE_2___default.a.number
};
MultipleSelectLabel.defaultProps = {
  noneSelectedLabel: NO_SELECTED_LABEL,
  maxDisplayItemCount: MAX_SELECTED_ITEM_FOR_DISPLAY
};
/* harmony default export */ __webpack_exports__["default"] = (MultipleSelectLabel);

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelectOptionList.jsx":
/*!*******************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelectOptionList.jsx ***!
  \*******************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(prop_types__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _Option__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./Option */ "./Areas/Portal/www/source/js/components/multiple-select/components/Option.jsx");






var MultipleSelectOptionList = function MultipleSelectOptionList(props) {
  var dataSource = props.dataSource,
      onChange = props.onChange,
      id = props.id,
      isDisableOption = props.isDisableOption;
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement("ul", {
    className: "multiple-select-options"
  }, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default()(dataSource).call(dataSource, function (item, index) {
    var _context, _context2;

    return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement(_Option__WEBPACK_IMPORTED_MODULE_4__["default"], {
      id: _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context = "".concat(id, "-optionItem-")).call(_context, index),
      key: _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_0___default()(_context2 = "".concat(id, "optionItem")).call(_context2, index),
      itemData: item,
      onChange: onChange,
      isDisableOption: isDisableOption
    });
  }));
};

MultipleSelectOptionList.propTypes = {
  dataSource: prop_types__WEBPACK_IMPORTED_MODULE_3___default.a.arrayOf(prop_types__WEBPACK_IMPORTED_MODULE_3___default.a.object).isRequired,
  id: prop_types__WEBPACK_IMPORTED_MODULE_3___default.a.oneOfType([prop_types__WEBPACK_IMPORTED_MODULE_3___default.a.number, prop_types__WEBPACK_IMPORTED_MODULE_3___default.a.string]).isRequired,
  onChange: prop_types__WEBPACK_IMPORTED_MODULE_3___default.a.func
};
/* harmony default export */ __webpack_exports__["default"] = (MultipleSelectOptionList);

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/multiple-select/components/Option.jsx":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/multiple-select/components/Option.jsx ***!
  \*************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(prop_types__WEBPACK_IMPORTED_MODULE_1__);



var Option = function Option(props) {
  var itemData = props.itemData,
      onChange = props.onChange,
      id = props.id,
      isDisableOption = props.isDisableOption;

  var handleChange = function handleChange(event) {
    event.stopPropagation();
    var newItemData = {
      key: itemData.key,
      value: itemData.value,
      checked: !itemData.checked
    };
    onChange && onChange(newItemData);
  };

  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("li", {
    className: "multiple-select-item"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("input", {
    id: id,
    type: "checkbox",
    className: "option-checkbox",
    checked: itemData.checked,
    onChange: handleChange,
    disabled: isDisableOption
  }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("label", {
    className: "option-label",
    htmlFor: id
  }, itemData.value));
};

Option.propTypes = {
  itemData: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.object.isRequired,
  id: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.oneOfType([prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.number, prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.string]).isRequired,
  onChange: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.func,
  isDisableOption: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.bool
};
/* harmony default export */ __webpack_exports__["default"] = (Option);

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/multiple-select/components/OptionAll.jsx":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/multiple-select/components/OptionAll.jsx ***!
  \****************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! prop-types */ "./node_modules/prop-types/index.js");
/* harmony import */ var prop_types__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(prop_types__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _Option__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./Option */ "./Areas/Portal/www/source/js/components/multiple-select/components/Option.jsx");



var LABEL = "All";
var DEFAULT_CHECKED = false;

var OptionAll = function OptionAll(props) {
  var id = props.id,
      checked = props.checked,
      label = props.label,
      onChange = props.onChange;
  var itemData = {
    key: "All",
    value: label,
    checked: checked
  };

  var changeHandler = function changeHandler(_ref) {
    var checked = _ref.checked;
    onChange && onChange(checked);
  };

  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("ul", {
    className: "multiple-select-options multiple-select-option-all"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_Option__WEBPACK_IMPORTED_MODULE_2__["default"], {
    id: "".concat(id, "-optionItemAll"),
    itemData: itemData,
    onChange: changeHandler
  }));
};

OptionAll.propTypes = {
  onChange: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.func,
  id: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.oneOfType([prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.number, prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.string]).isRequired,
  label: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.string,
  checked: prop_types__WEBPACK_IMPORTED_MODULE_1___default.a.bool
};
OptionAll.defaultProps = {
  label: LABEL,
  checked: DEFAULT_CHECKED
};
/* harmony default export */ __webpack_exports__["default"] = (OptionAll);

/***/ }),

/***/ "./Areas/Portal/www/source/js/components/multiple-select/index.jsx":
/*!*************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/components/multiple-select/index.jsx ***!
  \*************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _components_MultipleSelect__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./components/MultipleSelect */ "./Areas/Portal/www/source/js/components/multiple-select/components/MultipleSelect.jsx");

/* harmony default export */ __webpack_exports__["default"] = (_components_MultipleSelect__WEBPACK_IMPORTED_MODULE_0__["default"]);

/***/ }),

/***/ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/source/js/layouts/AppLayout.jsx ***!
  \**********************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return AppLayout; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var react_dom__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! react-dom */ "./node_modules/react-dom/index.js");
/* harmony import */ var react_dom__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(react_dom__WEBPACK_IMPORTED_MODULE_7__);







function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }




var AppLayout = /*#__PURE__*/function (_Component) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3___default()(AppLayout, _Component);

  var _super = _createSuper(AppLayout);

  function AppLayout(props) {
    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default()(this, AppLayout);

    return _super.call(this, props);
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default()(AppLayout, [{
    key: "render",
    value: function render() {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.createElement("div", {
        className: "app-container"
      }, this.props.children);
    }
  }]);

  return AppLayout;
}(react__WEBPACK_IMPORTED_MODULE_6__["Component"]);



/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/ajaxRequest.js":
/*!***********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/ajaxRequest.js ***!
  \***********************************************************/
/*! exports provided: post */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "post", function() { return post; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_promise__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/promise */ "./node_modules/@babel/runtime-corejs3/core-js-stable/promise.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_promise__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_promise__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var axios__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! axios */ "./node_modules/axios/index.js");
/* harmony import */ var axios__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(axios__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _utils__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./utils */ "./Areas/Portal/www/source/js/modules/utils.js");



var _xsrfToken = null;

var getXsrfToken = function getXsrfToken() {
  if (!_xsrfToken) {
    _xsrfToken = document.querySelector("input[name=__RequestVerificationToken]").value;
  }

  return _xsrfToken;
};

axios__WEBPACK_IMPORTED_MODULE_1___default.a.interceptors.response.use(function (response) {
  return _babel_runtime_corejs3_core_js_stable_promise__WEBPACK_IMPORTED_MODULE_0___default.a.resolve(response);
}, function (error) {
  var response = error.response;

  if (response) {
    var data = response.data;
    var status = data && data.ErrCode || response.status;

    switch (status) {
      case 401:
      case 403:
        alert("You are not authorized on this function.");
        location.reload(true);
        break;

      case 500:
        alert("An error occurred while processing your request");
        break;

      default:
        break;
    }
  }

  return _babel_runtime_corejs3_core_js_stable_promise__WEBPACK_IMPORTED_MODULE_0___default.a.reject(error);
});
var post = function post(relativeUrl, params, options) {
  var url = Object(_utils__WEBPACK_IMPORTED_MODULE_2__["resolveClientUrl"])(relativeUrl);
  return axios__WEBPACK_IMPORTED_MODULE_1___default.a.post(url, params, {
    headers: {
      'X-Requested-With': 'XMLHttpRequest',
      '__RequestVerificationToken': getXsrfToken()
    }
  }).then(function (response) {
    return response;
  }).catch(function (err) {
    options && options.onError && options.onError(err);
  });
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/constants.js":
/*!*********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/constants.js ***!
  \*********************************************************/
/*! exports provided: defaultPagingOption, minMonthDate, shortedMonthNames, monthNames, money, selectedAllProduct, maxWidth, spinnerLoaderWidth, spinnerLoaderUserPanelHeight, minDate, submitFail, pcLaptop, mobile, deviceTypes, regExp, changePasswordMessages */
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
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "submitFail", function() { return submitFail; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "pcLaptop", function() { return pcLaptop; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "mobile", function() { return mobile; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deviceTypes", function() { return deviceTypes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "regExp", function() { return regExp; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "changePasswordMessages", function() { return changePasswordMessages; });
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

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/createReducer.js":
/*!*************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/createReducer.js ***!
  \*************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return createReducer; });
function createReducer(initialState, handlers) {
  return function reducer() {
    var state = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : initialState;
    var action = arguments.length > 1 ? arguments[1] : undefined;

    if (handlers.hasOwnProperty(action.type)) {
      return handlers[action.type](state, action);
    } else {
      return state;
    }
  };
}

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/globalResource.js":
/*!**************************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/globalResource.js ***!
  \**************************************************************/
/*! exports provided: globalResource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return globalResource; });
var globalResource = {
  Index: '#',
  From: 'From',
  To: 'To',
  GetAll: 'Get All',
  Search: 'Search',
  SearchBy: 'Search By',
  All: 'All',
  Product: 'Product',
  Add: 'Add',
  Edit: 'Edit',
  Save: 'Save',
  Update: 'Update',
  Delete: 'Delete',
  Cancel: 'Cancel',
  Remove: 'Remove',
  NoInfo: 'There is no information available',
  ChooseFile: "Choose File",
  AnError: "An error occurred while processing your request, please try again later!"
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/htmlHelper.js":
/*!**********************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/htmlHelper.js ***!
  \**********************************************************/
/*! exports provided: htmlDecode */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "htmlDecode", function() { return htmlDecode; });
var htmlDecode = function htmlDecode(text) {
  var span = document.createElement("span");
  span.innerHTML = text;
  return span.textContent;
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/modules/store.js":
/*!*****************************************************!*\
  !*** ./Areas/Portal/www/source/js/modules/store.js ***!
  \*****************************************************/
/*! exports provided: configureStore */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "configureStore", function() { return configureStore; });
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var redux_thunk__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! redux-thunk */ "./node_modules/redux-thunk/es/index.js");


var middlewares = [];
middlewares.push(redux_thunk__WEBPACK_IMPORTED_MODULE_1__["default"]);

if (true) {
  var _require = __webpack_require__(/*! redux-logger */ "./node_modules/redux-logger/dist/redux-logger.js"),
      logger = _require.logger;

  middlewares.push(logger);
}

var configureStore = function configureStore(initialState, reducer) {
  var enhancer = Object(redux__WEBPACK_IMPORTED_MODULE_0__["compose"])(redux__WEBPACK_IMPORTED_MODULE_0__["applyMiddleware"].apply(void 0, middlewares));
  return Object(redux__WEBPACK_IMPORTED_MODULE_0__["createStore"])(reducer, initialState, enhancer);
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/components/ButtonDeleteItems.jsx":
/*!**********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/components/ButtonDeleteItems.jsx ***!
  \**********************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return ButtonDeleteItems; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/filter */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/filter.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _NotificationPopup__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./NotificationPopup */ "./Areas/Portal/www/source/js/pages/common/components/NotificationPopup.jsx");













function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_10___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_9___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }





var ButtonDeleteItems = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_8___default()(ButtonDeleteItems, _PureComponent);

  var _super = _createSuper(ButtonDeleteItems);

  function ButtonDeleteItems(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_5___default()(this, ButtonDeleteItems);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_getCheckedItemIdList", function () {
      var _context;

      var checkedItems = _this.props.checkedItems;
      return _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_4___default()(_context = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_3___default()(checkedItems)).call(_context, function (key) {
        return checkedItems[key];
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_handleOpenNotificationPopup", function (message, isConfirmPopup, itemIdList) {
      _this.setState({
        message: message,
        isConfirmPopup: isConfirmPopup || false,
        itemIdList: itemIdList || ''
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_handleCloseNotificationPopup", function (confirmResult) {
      _this.setState({
        message: ''
      });

      var itemIdList = _this.state.itemIdList;

      if (confirmResult && itemIdList) {
        _this._deleteExcute(itemIdList);
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_handleDelete", function () {
      var itemIdList = _this._getCheckedItemIdList();

      var length = itemIdList.length;
      length === 0 ? _this._handleOpenNotificationPopup(_this.props.messages.PleaseSelect) : _this._handleOpenNotificationPopup(_this.props.messages.AreYouSureDeleteItems.replace('{0}', length), true, itemIdList);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_11___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_7___default()(_this), "_deleteExcute", /*#__PURE__*/function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.mark(function _callee(itemIdList) {
        var result;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_1___default.a.wrap(function _callee$(_context2) {
          while (1) {
            switch (_context2.prev = _context2.next) {
              case 0:
                _this.props.toggleLoader(true);

                _context2.next = 3;
                return _this.props.deleteItems(itemIdList.join(','));

              case 3:
                result = _context2.sent;

                if (result) {
                  if (result.IsSuccessful) {
                    _this.props.isOpenNotification && _this._handleOpenNotificationPopup(_this.props.messages.DeleteItemsSuccessfully);

                    _this.props.fetchData();
                  } else {
                    _this._handleOpenNotificationPopup(result.Message);
                  }
                }

                _this.props.toggleLoader(false);

              case 6:
              case "end":
                return _context2.stop();
            }
          }
        }, _callee);
      }));

      return function (_x) {
        return _ref.apply(this, arguments);
      };
    }());

    _this.state = {
      itemIdList: '',
      message: '',
      isConfirmPopup: false
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_6___default()(ButtonDeleteItems, [{
    key: "render",
    value: function render() {
      var _this$state = this.state,
          message = _this$state.message,
          isConfirmPopup = _this$state.isConfirmPopup;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_12___default.a.Fragment, null, !!message && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(_NotificationPopup__WEBPACK_IMPORTED_MODULE_14__["default"], {
        message: message,
        isConfirmPopup: isConfirmPopup,
        onClose: this._handleCloseNotificationPopup
      }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_13__["Button"], {
        outline: true,
        type: "button",
        color: "danger",
        onClick: this._handleDelete
      }, "Delete", /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_12___default.a.createElement("span", {
        className: this.props.hasIcon ? "oi oi-trash ml-2" : ""
      })));
    }
  }]);

  return ButtonDeleteItems;
}(react__WEBPACK_IMPORTED_MODULE_12__["PureComponent"]);



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

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/components/SelectAll.jsx":
/*!**************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/components/SelectAll.jsx ***!
  \**************************************************************************/
/*! exports provided: SelectAll, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SelectAll", function() { return SelectAll; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/reduce */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_constants_selectAll__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../modules/constants/selectAll */ "./Areas/Portal/www/source/js/pages/common/modules/constants/selectAll.js");





var SelectAll = function SelectAll(props) {
  function _onCheckAll(isCheckAll) {
    var _context;

    var checkedList = props.checkedList;

    var newCheckedList = _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_1___default()(_context = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_0___default()(checkedList)).call(_context, function (accumulator, currentValue) {
      accumulator[currentValue] = isCheckAll;
      return accumulator;
    }, {});

    props.setShouldResetScrollPosition(false);
    props.setCheckedList(newCheckedList);
  }

  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement("div", {
    className: "d-flex align-items-center"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement("span", null, _modules_constants_selectAll__WEBPACK_IMPORTED_MODULE_4__["resource"].Select, ":"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_3__["Button"], {
    type: "button",
    color: "link",
    className: "text-dark font-weight-bold",
    onClick: function onClick() {
      return _onCheckAll(true);
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement("u", null, _modules_constants_selectAll__WEBPACK_IMPORTED_MODULE_4__["resource"].All)), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_3__["Button"], {
    type: "button",
    color: "link",
    className: "text-dark font-weight-bold",
    onClick: function onClick() {
      return _onCheckAll(false);
    }
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.createElement("u", null, _modules_constants_selectAll__WEBPACK_IMPORTED_MODULE_4__["resource"].None)));
};
/* harmony default export */ __webpack_exports__["default"] = (/*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_2___default.a.memo(SelectAll));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/common/modules/constants/selectAll.js":
/*!********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/common/modules/constants/selectAll.js ***!
  \********************************************************************************/
/*! exports provided: resource */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resource", function() { return resource; });
var resource = {
  Select: 'Select',
  All: 'All',
  None: 'None'
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js ***!
  \*************************************************************************************/
/*! exports provided: toggleLoader, setPagingOption, setParam, setParamFromHome, setBannerDetailUrl, setSelectedBannerId, setCheckedBanners, setShouldResetScrollPosition, setGridData, setSizeList, getSizeList, fetchData, getBannerDetail, insertBanner, updateBanner, deleteBanners */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setParam", function() { return setParam; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setParamFromHome", function() { return setParamFromHome; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setBannerDetailUrl", function() { return setBannerDetailUrl; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setSelectedBannerId", function() { return setSelectedBannerId; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setCheckedBanners", function() { return setCheckedBanners; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setShouldResetScrollPosition", function() { return setShouldResetScrollPosition; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setGridData", function() { return setGridData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "setSizeList", function() { return setSizeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getSizeList", function() { return getSizeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "fetchData", function() { return fetchData; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "getBannerDetail", function() { return getBannerDetail; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "insertBanner", function() { return insertBanner; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "updateBanner", function() { return updateBanner; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deleteBanners", function() { return deleteBanners; });
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../../modules/ajaxRequest */ "./Areas/Portal/www/source/js/modules/ajaxRequest.js");
/* harmony import */ var _actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../../actions/loaderAction */ "./Areas/Portal/www/source/js/actions/loaderAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "toggleLoader", function() { return _actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"]; });

/* harmony import */ var _actions_pagingAction__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../actions/pagingAction */ "./Areas/Portal/www/source/js/actions/pagingAction.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "setPagingOption", function() { return _actions_pagingAction__WEBPACK_IMPORTED_MODULE_5__["setPagingOption"]; });

/* harmony import */ var _types__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./types */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/types.js");








var setParam = function setParam(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_PARAM"],
    data: data
  };
};
var setParamFromHome = function setParamFromHome(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_PARAM_FROM_HOME"],
    data: data
  };
};
var setBannerDetailUrl = function setBannerDetailUrl(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_BANNER_DETAIL_URL"],
    data: data
  };
};
var setSelectedBannerId = function setSelectedBannerId(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_SELECTED_BANNER_ID"],
    data: data
  };
};
var setCheckedBanners = function setCheckedBanners(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_CHECKED_BANNERS"],
    data: data
  };
};
var setShouldResetScrollPosition = function setShouldResetScrollPosition(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SHOULD_RESET_SCROLL_POSITION"],
    data: data
  };
};
var setGridData = function setGridData(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_GRID_DATA"],
    data: data
  };
};
var setSizeList = function setSizeList(data) {
  return {
    type: _types__WEBPACK_IMPORTED_MODULE_6__["SET_SIZE_LIST"],
    data: data
  };
};
var getSizeList = function getSizeList(param) {
  return /*#__PURE__*/function () {
    var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee(dispatch) {
      var _context;

      var url, response, data;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee$(_context2) {
        while (1) {
          switch (_context2.prev = _context2.next) {
            case 0:
              url = 'Portal/MediaGallery/GetSizeList';
              _context2.next = 3;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__["post"])(url, param);

            case 3:
              response = _context2.sent;
              data = response && response.status === 200 ? _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_1___default()(_context = response.data).call(_context, function (item) {
                return {
                  key: item.Id,
                  name: item.Name,
                  checked: false
                };
              }) : null;
              dispatch(setSizeList(data));

            case 6:
            case "end":
              return _context2.stop();
          }
        }
      }, _callee);
    }));

    return function (_x) {
      return _ref.apply(this, arguments);
    };
  }();
};
var fetchData = function fetchData(param) {
  return /*#__PURE__*/function () {
    var _ref2 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee2(dispatch) {
      var url, response, data, checkedBanners;
      return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee2$(_context3) {
        while (1) {
          switch (_context3.prev = _context3.next) {
            case 0:
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(true));
              url = 'Portal/MediaGallery/GetBannerList';
              _context3.next = 4;
              return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__["post"])(url, param);

            case 4:
              response = _context3.sent;
              data = response && response.status === 200 ? response.data : null;
              checkedBanners = data ? _convertCheckedBanners(data.MainData) : {};
              dispatch(setGridData(data ? data.MainData : []));
              dispatch(Object(_actions_pagingAction__WEBPACK_IMPORTED_MODULE_5__["setPagingOption"])({
                TotalItem: data ? data.TotalItem : 0,
                PageIndex: param.PageIndex,
                PageSize: param.PageSize
              }));
              dispatch(setShouldResetScrollPosition(true));
              dispatch(setCheckedBanners(checkedBanners));
              dispatch(Object(_actions_loaderAction__WEBPACK_IMPORTED_MODULE_4__["toggleLoader"])(false));

            case 12:
            case "end":
              return _context3.stop();
          }
        }
      }, _callee2);
    }));

    return function (_x2) {
      return _ref2.apply(this, arguments);
    };
  }();
};

function _convertCheckedBanners(dataList) {
  var checkedBanners = {};

  for (var i = 0; i < dataList.length; i++) {
    var itemId = dataList[i].BannerId;
    checkedBanners[itemId] = false;
  }

  return checkedBanners;
}

var getBannerDetail = function getBannerDetail(selectedBannerId) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee3() {
    var url, response;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee3$(_context4) {
      while (1) {
        switch (_context4.prev = _context4.next) {
          case 0:
            url = 'Portal/MediaGallery/GetBannerDetail';
            _context4.next = 3;
            return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__["post"])(url, {
              selectedBannerId: selectedBannerId
            });

          case 3:
            response = _context4.sent;
            return _context4.abrupt("return", response && response.status === 200 ? response.data : null);

          case 5:
          case "end":
            return _context4.stop();
        }
      }
    }, _callee3);
  }));
};
var insertBanner = function insertBanner(param) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee4() {
    var url;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee4$(_context5) {
      while (1) {
        switch (_context5.prev = _context5.next) {
          case 0:
            url = 'Portal/MediaGallery/insertBanner';
            _context5.next = 3;
            return handleExcute(url, param);

          case 3:
            return _context5.abrupt("return", _context5.sent);

          case 4:
          case "end":
            return _context5.stop();
        }
      }
    }, _callee4);
  }));
};
var updateBanner = function updateBanner(param) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee5() {
    var url;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee5$(_context6) {
      while (1) {
        switch (_context6.prev = _context6.next) {
          case 0:
            url = 'Portal/MediaGallery/UpdateBanner';
            _context6.next = 3;
            return handleExcute(url, param);

          case 3:
            return _context6.abrupt("return", _context6.sent);

          case 4:
          case "end":
            return _context6.stop();
        }
      }
    }, _callee5);
  }));
};

var handleExcute = /*#__PURE__*/function () {
  var _ref6 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee6(url, param) {
    var formData, key, files, i, response;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee6$(_context7) {
      while (1) {
        switch (_context7.prev = _context7.next) {
          case 0:
            formData = new FormData();

            for (key in param) {
              key !== 'files' && formData.append(key, param[key]);
            }

            files = param['files'];

            if (files) {
              for (i = 0; i < files.length; i++) {
                formData.append('files', files[i]);
              }
            }

            _context7.next = 6;
            return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__["post"])(url, formData);

          case 6:
            response = _context7.sent;
            return _context7.abrupt("return", response && response.status === 200 ? response.data : null);

          case 8:
          case "end":
            return _context7.stop();
        }
      }
    }, _callee6);
  }));

  return function handleExcute(_x3, _x4) {
    return _ref6.apply(this, arguments);
  };
}();

var deleteBanners = function deleteBanners(BannerIdList) {
  return /*#__PURE__*/_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_2___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.mark(function _callee7() {
    var url, response;
    return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_0___default.a.wrap(function _callee7$(_context8) {
      while (1) {
        switch (_context8.prev = _context8.next) {
          case 0:
            url = 'Portal/MediaGallery/DeleteBanners';
            _context8.next = 3;
            return Object(_modules_ajaxRequest__WEBPACK_IMPORTED_MODULE_3__["post"])(url, {
              BannerIdList: BannerIdList
            });

          case 3:
            response = _context8.sent;
            return _context8.abrupt("return", response && response.status === 200 ? response.data : null);

          case 5:
          case "end":
            return _context8.stop();
        }
      }
    }, _callee7);
  }));
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/types.js":
/*!*************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/types.js ***!
  \*************************************************************************************/
/*! exports provided: SET_PARAM, SET_PARAM_FROM_HOME, SET_GRID_DATA, SET_CHECKED_BANNERS, SET_BANNER_DETAIL_URL, SET_SELECTED_BANNER_ID, SHOULD_RESET_SCROLL_POSITION, SET_SIZE_LIST */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_PARAM", function() { return SET_PARAM; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_PARAM_FROM_HOME", function() { return SET_PARAM_FROM_HOME; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_GRID_DATA", function() { return SET_GRID_DATA; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_CHECKED_BANNERS", function() { return SET_CHECKED_BANNERS; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_BANNER_DETAIL_URL", function() { return SET_BANNER_DETAIL_URL; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_SELECTED_BANNER_ID", function() { return SET_SELECTED_BANNER_ID; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SHOULD_RESET_SCROLL_POSITION", function() { return SHOULD_RESET_SCROLL_POSITION; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SET_SIZE_LIST", function() { return SET_SIZE_LIST; });
var SET_PARAM = 'SET_PARAM';
var SET_PARAM_FROM_HOME = 'SET_PARAM_FROM_HOME';
var SET_GRID_DATA = 'SET_GRID_DATA';
var SET_CHECKED_BANNERS = 'SET_CHECKED_BANNERS';
var SET_BANNER_DETAIL_URL = 'SET_BANNER_DETAIL_URL';
var SET_SELECTED_BANNER_ID = 'SET_SELECTED_BANNER_ID';
var SHOULD_RESET_SCROLL_POSITION = 'SHOULD_RESET_SCROLL_POSITION';
var SET_SIZE_LIST = 'SET_SIZE_LIST';

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/AppContainer.jsx":
/*!************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/AppContainer.jsx ***!
  \************************************************************************************************/
/*! exports provided: AppContainer, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppContainer", function() { return AppContainer; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _layouts_AppLayout__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../../../layouts/AppLayout */ "./Areas/Portal/www/source/js/layouts/AppLayout.jsx");
/* harmony import */ var _components_Loader__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../components/Loader */ "./Areas/Portal/www/source/js/components/Loader.jsx");
/* harmony import */ var _Filter__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./Filter */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/Filter.jsx");
/* harmony import */ var _common_components_ControlButtons__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ../../common/components/ControlButtons */ "./Areas/Portal/www/source/js/pages/media-gallery/common/components/ControlButtons.jsx");
/* harmony import */ var _Content__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./Content */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/Content.jsx");
/* harmony import */ var _EditBannerForm__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./EditBannerForm */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/EditBannerForm.jsx");
/* harmony import */ var _BannerDetailPopup__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./BannerDetailPopup */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/BannerDetailPopup.jsx");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");














var AppContainer = function AppContainer(props) {
  return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_layouts_AppLayout__WEBPACK_IMPORTED_MODULE_4__["default"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_components_Loader__WEBPACK_IMPORTED_MODULE_5__["default"], {
    show: props.isShowLoader
  }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_Filter__WEBPACK_IMPORTED_MODULE_6__["default"], null), props.isModifiable && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_common_components_ControlButtons__WEBPACK_IMPORTED_MODULE_7__["default"], {
    messages: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["messages"],
    gridData: props.gridData,
    fetchData: function fetchData() {
      return props.fetchData(props.param);
    },
    checkedItems: props.checkedBanners,
    setCheckedItems: props.setCheckedBanners,
    toggleLoader: props.toggleLoader,
    setShouldResetScrollPosition: props.setShouldResetScrollPosition,
    setSelectedItemId: props.setSelectedBannerId,
    deleteItems: props.deleteBanners
  }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_Content__WEBPACK_IMPORTED_MODULE_8__["default"], null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_EditBannerForm__WEBPACK_IMPORTED_MODULE_9__["default"], null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(_BannerDetailPopup__WEBPACK_IMPORTED_MODULE_10__["default"], null));
};

function mapStateToProps(_ref) {
  var isShowLoader = _ref.isShowLoader,
      param = _ref.param,
      gridData = _ref.gridData,
      checkedBanners = _ref.checkedBanners,
      claimsPrincipal = _ref.claimsPrincipal;
  var isModifiable = claimsPrincipal.hasWriteClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_13__["claimResource"].MediaGallery);
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    isShowLoader: isShowLoader,
    param: param,
    gridData: gridData,
    checkedBanners: checkedBanners,
    isModifiable: isModifiable
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_3__["bindActionCreators"])({
    setParam: _actions__WEBPACK_IMPORTED_MODULE_12__["setParam"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_12__["fetchData"],
    toggleLoader: _actions__WEBPACK_IMPORTED_MODULE_12__["toggleLoader"],
    setSelectedBannerId: _actions__WEBPACK_IMPORTED_MODULE_12__["setSelectedBannerId"],
    setCheckedBanners: _actions__WEBPACK_IMPORTED_MODULE_12__["setCheckedBanners"],
    deleteBanners: _actions__WEBPACK_IMPORTED_MODULE_12__["deleteBanners"],
    setShouldResetScrollPosition: _actions__WEBPACK_IMPORTED_MODULE_12__["setShouldResetScrollPosition"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_2__["connect"])(mapStateToProps, mapDispatchToProps)( /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.memo(AppContainer)));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/BannerDetailPopup.jsx":
/*!*****************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/BannerDetailPopup.jsx ***!
  \*****************************************************************************************************/
/*! exports provided: BannerDetailPopup, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BannerDetailPopup", function() { return BannerDetailPopup; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions_index__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../actions/index */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js");
/* harmony import */ var _libs_utils__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../libs/utils */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/utils.js");







var maxWidthModal = 1108;
var BannerDetailPopup = function BannerDetailPopup(props) {
  var bannerDetailUrl = props.bannerDetailUrl;

  var _handleClosePopup = function _handleClosePopup() {
    props.setBannerDetailUrl({
      src: null
    });
  };

  var resizeResult = bannerDetailUrl && Object(_libs_utils__WEBPACK_IMPORTED_MODULE_6__["resizeWidthHeight"])(bannerDetailUrl.width, bannerDetailUrl.height, maxWidthModal);
  return bannerDetailUrl && bannerDetailUrl.src && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_4__["Modal"], {
    isOpen: !!bannerDetailUrl.src,
    toggle: _handleClosePopup,
    size: "xl",
    backdrop: "static"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_4__["ModalHeader"], {
    toggle: _handleClosePopup
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("span", {
    className: "iconpopup"
  }), bannerDetailUrl.bannerId), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_4__["ModalBody"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("div", {
    className: "modal-body-wrapper embed-responsive overflow-auto text-center"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.createElement("img", {
    src: bannerDetailUrl.src,
    alt: "12Bet banner",
    width: resizeResult.width,
    height: resizeResult.height
  }))));
};

function mapStateToProps(_ref) {
  var bannerDetailUrl = _ref.bannerDetailUrl;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_0___default()({}, {
    bannerDetailUrl: bannerDetailUrl
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_3__["bindActionCreators"])({
    setBannerDetailUrl: _actions_index__WEBPACK_IMPORTED_MODULE_5__["setBannerDetailUrl"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_2__["connect"])(mapStateToProps, mapDispatchToProps)( /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_1___default.a.memo(BannerDetailPopup)));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/Content.jsx":
/*!*******************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/Content.jsx ***!
  \*******************************************************************************************/
/*! exports provided: Content, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Content", function() { return Content; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @9softstudio/react-fixed-table-header */ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js");
/* harmony import */ var _9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _libs_utils__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ../libs/utils */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/utils.js");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js");
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");














var Content = function Content(props) {
  Object(react__WEBPACK_IMPORTED_MODULE_4__["useEffect"])(function () {
    var param = props.param;
    param && props.fetchData(param);
  }, [props.param]);

  var _onCheckItem = function _onCheckItem(isChecked, BannerId) {
    var newCheckedBanners = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, props.checkedBanners, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_2___default()({}, BannerId, isChecked));

    props.setShouldResetScrollPosition(false);
    props.setCheckedBanners(newCheckedBanners);
  };

  var _handlePaging = function _handlePaging(pageIndex, pageSize) {
    var newPaging = {
      PageIndex: pageIndex,
      PageSize: pageSize
    };

    var newParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, props.param, newPaging);

    props.setPagingOption(newPaging);
    props.setParam(newParam);
  };

  function _renderHeader() {
    var isModifiable = props.isModifiable;
    return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Row"], {
      key: "thead"
    }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 20
    }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 25,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Index
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Index), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 415,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Images
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Images), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 190,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].FileName
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].FileName), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 60,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Category
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Category), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 55,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].SubCategory
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].SubCategory), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 60,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Language
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Language), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 55,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Currency
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Currency), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 50,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Device
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Device), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 50,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].ImageType
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].ImageType), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 50,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Size
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["headerText"].Size), isModifiable && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      header: true,
      colWidth: 25,
      title: _libs_constants__WEBPACK_IMPORTED_MODULE_11__["globalResource"].Edit
    }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["globalResource"].Edit));
  }

  function _renderDataSection(dataList) {
    var dataRows = [];

    if (dataList && dataList.length) {
      var pagingOption = props.pagingOption;
      var startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

      for (var i = 0; i < dataList.length; i++) {
        var dataItem = dataList[i];
        dataRows.push(_buildDataRow(dataItem, i + startIndex));
      }
    } else {
      var header = _renderHeader();

      var colmCount = header.props.children.length;
      dataRows.push( /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Row"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
        className: "text-center",
        colSpan: colmCount
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_11__["globalResource"].NoInfo)));
    }

    return dataRows;
  }

  function _buildDataRow(dataItem, index) {
    var _context, _context2, _context3, _context4, _context5, _context6;

    var isModifiable = props.isModifiable;
    var bannerId = dataItem.BannerId;
    var fileName = dataItem.FileName;

    var src = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_1___default()(_context2 = "".concat(props.bannerRootUrl, "/")).call(_context2, fileName, "?id=")).call(_context, dataItem.LastUpdateText);

    var size = dataItem.Size;
    var strSize = size.split('x');
    var realWidth = strSize[0];
    var realHeight = strSize[1];
    var isValidWithHeight = realWidth < _libs_constants__WEBPACK_IMPORTED_MODULE_11__["maxWidthHeight"] && realHeight < _libs_constants__WEBPACK_IMPORTED_MODULE_11__["maxWidthHeight"];
    var resizeResult = !isValidWithHeight && Object(_libs_utils__WEBPACK_IMPORTED_MODULE_10__["resizeWidthHeight"])(realWidth, realHeight);
    return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Row"], {
      key: "dataRow-".concat(index)
    }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center"
    }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("input", {
      type: "checkbox",
      checked: props.checkedBanners[bannerId],
      onChange: function onChange(e) {
        return _onCheckItem(e.target.checked, bannerId);
      }
    })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center",
      title: index
    }, index), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center"
    }, isValidWithHeight ? /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("img", {
      id: "img".concat(bannerId),
      src: src,
      alt: "",
      height: realHeight,
      width: realWidth
    }) : /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("a", {
      href: "javascript:;",
      id: "link".concat(bannerId),
      onClick: function onClick() {
        return props.setBannerDetailUrl({
          src: src,
          bannerId: bannerId,
          width: realWidth,
          height: realHeight
        });
      }
    }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("img", {
      id: "img".concat(bannerId),
      src: src,
      alt: "12Bet banner",
      height: resizeResult.height,
      width: resizeResult.width
    }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break",
      title: fileName
    }, fileName), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break"
    }, dataItem.Categories && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(_context3 = dataItem.Categories).call(_context3, function (value) {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("p", {
        key: "category-".concat(value),
        value: value
      }, value);
    })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break"
    }, dataItem.SubCategories && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(_context4 = dataItem.SubCategories).call(_context4, function (value) {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("p", {
        key: "subCategory-".concat(value)
      }, value);
    })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break",
      title: dataItem.Language
    }, dataItem.Language), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break"
    }, dataItem.Currencies && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(_context5 = dataItem.Currencies).call(_context5, function (value) {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("p", {
        key: "currency-".concat(value)
      }, value);
    })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break"
    }, dataItem.DeviceTypes && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_0___default()(_context6 = dataItem.DeviceTypes).call(_context6, function (value) {
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("p", {
        key: "deviceType-".concat(value)
      }, value == 0 ? _libs_constants__WEBPACK_IMPORTED_MODULE_11__["pcLaptop"] : _libs_constants__WEBPACK_IMPORTED_MODULE_11__["mobile"]);
    })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break",
      title: dataItem.ImageType
    }, dataItem.ImageType), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center text-break",
      title: size
    }, size), isModifiable && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Cell"], {
      className: "text-center",
      title: "Edit Banner"
    }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_8__["Button"], {
      color: "link",
      onClick: function onClick() {
        return props.setSelectedBannerId(bannerId);
      }
    }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("span", {
      className: "oi oi-pencil text-secondary"
    }))));
  }

  return props.gridData && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("div", {
    className: "data-content"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement("div", {
    className: "banner-list-content table-responsive table-striped my-2"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.createElement(_9softstudio_react_fixed_table_header__WEBPACK_IMPORTED_MODULE_7__["Table"], {
    minWidth: 1250,
    autoWidth: true,
    bodyHeight: 300,
    header: _renderHeader(),
    body: _renderDataSection(props.gridData),
    containerPadding: 20,
    isPaging: true,
    pageOption: props.pagingOption,
    onPaging: _handlePaging,
    shouldResetScrollPosition: props.shouldResetScrollPosition
  })));
};

function mapStateToProps(_ref) {
  var pagingOption = _ref.pagingOption,
      param = _ref.param,
      checkedBanners = _ref.checkedBanners,
      bannerRootUrl = _ref.bannerRootUrl,
      shouldResetScrollPosition = _ref.shouldResetScrollPosition,
      gridData = _ref.gridData,
      claimsPrincipal = _ref.claimsPrincipal;
  var isModifiable = claimsPrincipal.hasWriteClaim(_modules_claim__WEBPACK_IMPORTED_MODULE_13__["claimResource"].MediaGallery);
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
    pagingOption: pagingOption,
    param: param,
    checkedBanners: checkedBanners,
    bannerRootUrl: bannerRootUrl,
    shouldResetScrollPosition: shouldResetScrollPosition,
    gridData: gridData,
    isModifiable: isModifiable
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_6__["bindActionCreators"])({
    setSelectedBannerId: _actions__WEBPACK_IMPORTED_MODULE_12__["setSelectedBannerId"],
    fetchData: _actions__WEBPACK_IMPORTED_MODULE_12__["fetchData"],
    setPagingOption: _actions__WEBPACK_IMPORTED_MODULE_12__["setPagingOption"],
    setCheckedBanners: _actions__WEBPACK_IMPORTED_MODULE_12__["setCheckedBanners"],
    setShouldResetScrollPosition: _actions__WEBPACK_IMPORTED_MODULE_12__["setShouldResetScrollPosition"],
    setParam: _actions__WEBPACK_IMPORTED_MODULE_12__["setParam"],
    setBannerDetailUrl: _actions__WEBPACK_IMPORTED_MODULE_12__["setBannerDetailUrl"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_5__["connect"])(mapStateToProps, mapDispatchToProps)( /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_4___default.a.memo(Content)));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/EditBannerForm.jsx":
/*!**************************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/EditBannerForm.jsx ***!
  \**************************************************************************************************/
/*! exports provided: EditBannerForm, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EditBannerForm", function() { return EditBannerForm; });
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
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/get-own-property-symbols */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-symbols.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/keys */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/reduce */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/index-of */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/index-of.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/trim */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/trim.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/slice */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/slice.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/concat */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_array_from__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/array/from */ "./node_modules/@babel/runtime-corejs3/core-js-stable/array/from.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_array_from__WEBPACK_IMPORTED_MODULE_15___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_array_from__WEBPACK_IMPORTED_MODULE_15__);
/* harmony import */ var _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/toConsumableArray */ "./node_modules/@babel/runtime-corejs3/helpers/toConsumableArray.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16__);
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! @babel/runtime-corejs3/regenerator */ "./node_modules/@babel/runtime-corejs3/regenerator/index.js");
/* harmony import */ var _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_includes__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/includes */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/includes.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_includes__WEBPACK_IMPORTED_MODULE_18___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_includes__WEBPACK_IMPORTED_MODULE_18__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/filter */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/filter.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/last-index-of */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/last-index-of.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_20___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_20__);
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/asyncToGenerator */ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_21___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_21__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_22__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_22___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_22__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_23__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_23___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_23__);
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/assertThisInitialized */ "./node_modules/@babel/runtime-corejs3/helpers/assertThisInitialized.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_25__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_25___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_25__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_26__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_26___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_26__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_27__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_27___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_27__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_29__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_29___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_29__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_30__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_31__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_32__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _actions_index__WEBPACK_IMPORTED_MODULE_33__ = __webpack_require__(/*! ../actions/index */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_34__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js");
/* harmony import */ var _common_libs_constants__WEBPACK_IMPORTED_MODULE_35__ = __webpack_require__(/*! ../../common/libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/common/libs/constants.js");
/* harmony import */ var _modules_htmlHelper__WEBPACK_IMPORTED_MODULE_36__ = __webpack_require__(/*! ../../../../modules/htmlHelper */ "./Areas/Portal/www/source/js/modules/htmlHelper.js");
/* harmony import */ var _common_components_NotificationPopup__WEBPACK_IMPORTED_MODULE_37__ = __webpack_require__(/*! ../../../common/components/NotificationPopup */ "./Areas/Portal/www/source/js/pages/common/components/NotificationPopup.jsx");
/* harmony import */ var _libs_utils__WEBPACK_IMPORTED_MODULE_38__ = __webpack_require__(/*! ../libs/utils */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/utils.js");
/* harmony import */ var _components_multiple_select__WEBPACK_IMPORTED_MODULE_39__ = __webpack_require__(/*! ../../../../components/multiple-select */ "./Areas/Portal/www/source/js/components/multiple-select/index.jsx");
/* harmony import */ var _common_components_InputFile__WEBPACK_IMPORTED_MODULE_40__ = __webpack_require__(/*! ../../common/components/InputFile */ "./Areas/Portal/www/source/js/pages/media-gallery/common/components/InputFile.jsx");






























function ownKeys(object, enumerableOnly) { var keys = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_6___default()(object); if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_5___default.a) { var symbols = _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_5___default()(object); if (enumerableOnly) symbols = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(symbols).call(symbols, function (sym) { return _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { var _context21; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context21 = ownKeys(Object(source), true)).call(_context21, function (key) { _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(target, key, source[key]); }); } else if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default.a) { _babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1___default()(target, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default()(source)); } else { var _context22; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context22 = ownKeys(Object(source))).call(_context22, function (key) { _babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0___default()(target, key, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(source, key)); }); } } return target; }

function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_27___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_27___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_26___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_7___default()(Date, [], function () {})); return true; } catch (e) { return false; } }













var regexSpecialCharacters = new RegExp(_libs_constants__WEBPACK_IMPORTED_MODULE_34__["regSpecialCharacters"]);
var EditBannerForm = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_25___default()(EditBannerForm, _PureComponent);

  var _super = _createSuper(EditBannerForm);

  function EditBannerForm(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_22___default()(this, EditBannerForm);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleGetBannerDetail", /*#__PURE__*/function () {
      var _ref = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_21___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default.a.mark(function _callee(selectedBannerId) {
        var bannerInfo, _this$props, categoryList, subCategoryList, currencyList, affTypeList, deviceTypeList, fileName, indexOfShortFileName, shortFileName;

        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default.a.wrap(function _callee$(_context2) {
          while (1) {
            switch (_context2.prev = _context2.next) {
              case 0:
                _context2.next = 2;
                return _this.props.getBannerDetail(selectedBannerId);

              case 2:
                bannerInfo = _context2.sent;

                if (bannerInfo) {
                  _this$props = _this.props, categoryList = _this$props.categoryList, subCategoryList = _this$props.subCategoryList, currencyList = _this$props.currencyList, affTypeList = _this$props.affTypeList, deviceTypeList = _this$props.deviceTypeList;
                  fileName = Object(_modules_htmlHelper__WEBPACK_IMPORTED_MODULE_36__["htmlDecode"])(bannerInfo.FileName);
                  indexOfShortFileName = _babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_20___default()(fileName).call(fileName, '/') + 1;
                  shortFileName = fileName.substring(indexOfShortFileName, fileName.length);

                  _this.setState({
                    inputFileNames: [shortFileName],
                    categoryList: _this.handleChangeMultipleSelected(categoryList, bannerInfo.Categories, true),
                    subCategoryList: _this.handleChangeMultipleSelected(_babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(subCategoryList).call(subCategoryList, function (s) {
                      var _context;

                      return _babel_runtime_corejs3_core_js_stable_instance_includes__WEBPACK_IMPORTED_MODULE_18___default()(_context = bannerInfo.Categories).call(_context, s.parentCategoryName);
                    }), bannerInfo.SubCategories),
                    language: bannerInfo.Language,
                    currencyList: _this.handleChangeMultipleSelected(currencyList, bannerInfo.Currencies),
                    affTypeList: _this.handleChangeMultipleSelected(affTypeList, bannerInfo.AffTypes),
                    deviceTypeList: _this.handleChangeMultipleSelected(deviceTypeList, bannerInfo.DeviceTypes),
                    iniatalFileName: shortFileName
                  });
                }

              case 4:
              case "end":
                return _context2.stop();
            }
          }
        }, _callee);
      }));

      return function (_x) {
        return _ref.apply(this, arguments);
      };
    }());

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleChange", function (event) {
      var value = event.target.value;
      var name = event.target.name;

      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()({}, name, value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleChangeInputFileName", function (event, index) {
      var value = event.target.value;

      if (regexSpecialCharacters.test(value)) {
        _this._handleShowAlert(false, "A File Name can't contain any of the following characters: \\/:*?\"<>|#%&+");
      } else {
        var inputFileNames = _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16___default()(_this.state.inputFileNames);

        inputFileNames[index] = value;

        _this.setState({
          inputFileNames: inputFileNames
        });
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleFile", function (event) {
      var files = event.target.files;

      if (files && files.length > 0) {
        var inputFileNames = [];
        var allResults = {
          isOK: true,
          message: ''
        };

        for (var i = 0; i < files.length; i++) {
          var fileName = files[i].name;
          inputFileNames.push(fileName);
          var result = Object(_libs_utils__WEBPACK_IMPORTED_MODULE_38__["validateFileExtension"])(fileName);

          if (!result.isOK) {
            allResults = result;
          }
        }

        _this.setState({
          files: _babel_runtime_corejs3_core_js_stable_array_from__WEBPACK_IMPORTED_MODULE_15___default()(files),
          inputFileNames: inputFileNames,
          isDisabled: !allResults.isOK
        });

        !allResults.isOK && _this._handleShowAlert(false, allResults.message);
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleRemoveImageAt", function (index) {
      var _context3, _context4;

      var inputFileNames = _this.state.inputFileNames;
      var files = _this.state.files;

      if (!files || files.length == 0 || files.length < index + 1) {
        return;
      }

      inputFileNames = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14___default()(_context3 = []).call(_context3, _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16___default()(_babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13___default()(inputFileNames).call(inputFileNames, 0, index)), _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16___default()(_babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13___default()(inputFileNames).call(inputFileNames, index + 1, inputFileNames.length)));
      files = _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14___default()(_context4 = []).call(_context4, _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16___default()(_babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13___default()(files).call(files, 0, index)), _babel_runtime_corejs3_helpers_toConsumableArray__WEBPACK_IMPORTED_MODULE_16___default()(_babel_runtime_corejs3_core_js_stable_instance_slice__WEBPACK_IMPORTED_MODULE_13___default()(files).call(files, index + 1, files.length)));

      if (files && files.length) {
        var allResults = {
          isOK: true,
          message: ''
        };

        for (var i = 0; i < files.length; i++) {
          var fileName = files[i].name;
          var result = Object(_libs_utils__WEBPACK_IMPORTED_MODULE_38__["validateFileExtension"])(fileName);

          if (!result.isOK) {
            allResults = result;
          }
        }

        _this.setState({
          files: files,
          inputFileNames: inputFileNames,
          isDisabled: !allResults.isOK
        });

        !allResults.isOK && _this._handleShowAlert(false, allResults.message);
      } else {
        _this.setState({
          inputFileNames: [''],
          files: files,
          isDisabled: true
        });
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleAfterSave", function (isInsert, result) {
      if (result) {
        result.IsSuccessful && _this.props.getSizeList(_this.props.param);
        var message = isInsert ? "Your banner has been added successfully!" : "Your banner has been updated successfully!";

        _this._handleShowAlert(result.IsSuccessful, result.IsSuccessful ? message : result.Message);
      } else {
        _this._handleShowAlert(false, isInsert ? "The add is failed!" : "The update is failed!");
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_save", /*#__PURE__*/function () {
      var _ref2 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_21___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default.a.mark(function _callee2(hasSubCategory) {
        var _this$state, inputFileNames, isInsert, iniatalFileName, bannerInfo, result;

        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default.a.wrap(function _callee2$(_context5) {
          while (1) {
            switch (_context5.prev = _context5.next) {
              case 0:
                _this$state = _this.state, inputFileNames = _this$state.inputFileNames, isInsert = _this$state.isInsert, iniatalFileName = _this$state.iniatalFileName;
                bannerInfo = _this._getParam(hasSubCategory);

                if (!isInsert) {
                  _context5.next = 9;
                  break;
                }

                _context5.next = 5;
                return _this.props.insertBanner(bannerInfo);

              case 5:
                result = _context5.sent;

                _this._handleAfterSave(isInsert, result);

                _context5.next = 10;
                break;

              case 9:
                if (inputFileNames[0] === iniatalFileName) {
                  _this.setState({
                    messageNotificationPopup: 'This file name exists already. Do you want to overwrite the current file?'
                  });
                } else {
                  _this._updateBanner(bannerInfo);
                }

              case 10:
                ;

              case 11:
              case "end":
                return _context5.stop();
            }
          }
        }, _callee2);
      }));

      return function (_x2) {
        return _ref2.apply(this, arguments);
      };
    }());

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_getParam", function (hasSubCategory) {
      var _context6, _context7, _context8, _context9, _context10, _context11;

      var _this$state2 = _this.state,
          files = _this$state2.files,
          inputFileNames = _this$state2.inputFileNames,
          categoryList = _this$state2.categoryList,
          subCategoryList = _this$state2.subCategoryList,
          language = _this$state2.language,
          currencyList = _this$state2.currencyList,
          affTypeList = _this$state2.affTypeList,
          deviceTypeList = _this$state2.deviceTypeList,
          isInsert = _this$state2.isInsert;

      var categories = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context6 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(categoryList).call(categoryList, function (item) {
        return item.checked;
      })).call(_context6, function (item) {
        return item.key;
      }).join(',');

      var subCategories = hasSubCategory ? _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context7 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(subCategoryList).call(subCategoryList, function (item) {
        return item.checked;
      })).call(_context7, function (item) {
        return item.key;
      }).join(',') : '';

      var currencies = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context8 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(currencyList).call(currencyList, function (item) {
        return item.checked;
      })).call(_context8, function (item) {
        return item.key;
      }).join(',');

      var affTypes = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context9 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(affTypeList).call(affTypeList, function (item) {
        return item.checked;
      })).call(_context9, function (item) {
        return item.key;
      }).join(',');

      var deviceTypes = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context10 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(deviceTypeList).call(deviceTypeList, function (item) {
        return item.checked;
      })).call(_context10, function (item) {
        return item.key;
      }).join(',');

      var selectedBannerId = _this.props.selectedBannerId;
      return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default()({}, {
        bannerId: selectedBannerId,
        files: files,
        inputFileNames: isInsert ? _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(inputFileNames).call(inputFileNames, function (item) {
          return _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10___default()(item).call(item);
        }).join('|') : inputFileNames[0] && _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10___default()(_context11 = inputFileNames[0]).call(_context11),
        categories: categories,
        subCategories: subCategories,
        language: language,
        currencies: currencies,
        affTypes: affTypes,
        deviceTypes: deviceTypes
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_updateBanner", /*#__PURE__*/function () {
      var _ref3 = _babel_runtime_corejs3_helpers_asyncToGenerator__WEBPACK_IMPORTED_MODULE_21___default()( /*#__PURE__*/_babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default.a.mark(function _callee3(bannerInfo) {
        var result;
        return _babel_runtime_corejs3_regenerator__WEBPACK_IMPORTED_MODULE_17___default.a.wrap(function _callee3$(_context12) {
          while (1) {
            switch (_context12.prev = _context12.next) {
              case 0:
                _context12.next = 2;
                return _this.props.updateBanner(bannerInfo);

              case 2:
                result = _context12.sent;

                _this._handleAfterSave(_this.state.isInsert, result);

              case 4:
              case "end":
                return _context12.stop();
            }
          }
        }, _callee3);
      }));

      return function (_x3) {
        return _ref3.apply(this, arguments);
      };
    }());

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_validateInputFileName", function () {
      var _this$state3 = _this.state,
          inputFileNames = _this$state3.inputFileNames,
          isInsert = _this$state3.isInsert,
          files = _this$state3.files;
      var validInputFileNames = {
        isOK: true,
        message: ''
      };

      if (isInsert) {
        for (var i = 0; i < inputFileNames.length; i++) {
          var inputFileName = inputFileNames[i];

          if (inputFileName) {
            var result = Object(_libs_utils__WEBPACK_IMPORTED_MODULE_38__["validateInputFile"])(_babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10___default()(inputFileName).call(inputFileName));

            if (!result.isOK) {
              result.message = "File Name ".concat(i + 1, " incorrect.<br />") + result.message;
              validInputFileNames = result;
            }
          } else {
            if (files && files.length > 0) {
              var _result = Object(_libs_utils__WEBPACK_IMPORTED_MODULE_38__["validateInputFile"])(files[i].name);

              if (!_result.isOK) {
                validInputFileNames = _result;
              }
            } else {
              validInputFileNames = {
                isOK: false,
                message: 'You must browse a banner.'
              };
            }
          }
        }
      } else {
        var _inputFileName = inputFileNames[0];

        if (_inputFileName) {
          var _result2 = Object(_libs_utils__WEBPACK_IMPORTED_MODULE_38__["validateInputFile"])(inputFileNames[0]);

          if (!_result2.isOK) {
            validInputFileNames = _result2;
          }
        } else {
          if (files && files.length > 0) {
            var _result3 = Object(_libs_utils__WEBPACK_IMPORTED_MODULE_38__["validateInputFile"])(files[0].name);

            if (!_result3.isOK) {
              validInputFileNames = _result3;
            }
          } else {
            validInputFileNames = {
              isOK: false,
              message: 'You must either enter a file name for banner or browse other banner.'
            };
          }
        }
      }

      return validInputFileNames;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_hasDuplicateFileNames", function (inputFileNames) {
      var length = inputFileNames.length;

      for (var i = 0; i <= length - 1; i++) {
        var _context13;

        var fileName = _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10___default()(_context13 = inputFileNames[i]).call(_context13).toLowerCase();

        for (var j = i + 1; j < length; j++) {
          var _context14;

          if (fileName == _babel_runtime_corejs3_core_js_stable_instance_trim__WEBPACK_IMPORTED_MODULE_10___default()(_context14 = inputFileNames[j]).call(_context14).toLowerCase()) {
            return true;
          }
        }
      }

      return false;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleSubmit", function (hasSubCategory) {
      var _context15;

      _this.props.toggleLoader(true);

      var _this$state4 = _this.state,
          categoryList = _this$state4.categoryList,
          subCategoryList = _this$state4.subCategoryList,
          currencyList = _this$state4.currencyList,
          affTypeList = _this$state4.affTypeList,
          deviceTypeList = _this$state4.deviceTypeList,
          inputFileNames = _this$state4.inputFileNames;

      var category = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context15 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(categoryList).call(categoryList, function (item) {
        return item.checked;
      })).call(_context15, function (item) {
        return item.key;
      });

      var subCategory = hasSubCategory && _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(subCategoryList).call(subCategoryList, function (item) {
        return item.checked;
      });

      var currency = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(currencyList).call(currencyList, function (item) {
        return item.checked;
      });

      var affType = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(affTypeList).call(affTypeList, function (item) {
        return item.checked;
      });

      var deviceType = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(deviceTypeList).call(deviceTypeList, function (item) {
        return item.checked;
      });

      if (!category.length) {
        _this._handleShowAlert(false, "Please select Category.");

        return;
      }

      if (hasSubCategory && (_babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9___default()(category).call(category, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["Casino"]) !== -1 || _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9___default()(category).call(category, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["Sportsbook"]) !== -1) && !subCategory.length) {
        _this._handleShowAlert(false, "Please select Sub Category.");

        return;
      }

      if (!currency.length) {
        _this._handleShowAlert(false, "Please select Currency.");

        return;
      }

      if (!affType.length) {
        _this._handleShowAlert(false, "Please select Affiliate Type.");

        return;
      }

      if (!deviceType.length) {
        _this._handleShowAlert(false, "Please select Device.");

        return;
      }

      if (inputFileNames.length > 10) {
        _this._handleShowAlert(false, "Can't upload exceed 10 images one time");

        return;
      }

      if (_this._hasDuplicateFileNames(inputFileNames)) {
        _this._handleShowAlert(false, "File Name is unique, please enter other file name!");

        return;
      }

      var validInputFileNames = _this._validateInputFileName();

      validInputFileNames.isOK ? _this._save(hasSubCategory) : _this._handleShowAlert(false, validInputFileNames.message);

      _this.props.toggleLoader(false);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleShowAlert", function (isSuccess, message) {
      _this.modalRef.current.scrollTo({
        top: 0,
        behavior: "smooth"
      });

      _this.setState({
        message: message,
        isSuccess: isSuccess,
        isShowMessage: true
      }, function () {
        return window.setTimeout(function () {
          return _this._hideMessage(isSuccess);
        }, 4000);
      });

      _this.props.toggleLoader(false);
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_hideMessage", function (isSuccess) {
      _this.setState({
        isShowMessage: false
      });

      if (isSuccess) {
        _this.props.fetchData(_this.props.param);

        _this._handleClosePopup();
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handleClosePopup", function () {
      _this.props.setSelectedBannerId(null);

      _this.setState(_objectSpread({}, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["defaultState"]));

      _this.resetToDefault();
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleCloseNotificationPopup", function (confirmResult, hasSubCategory) {
      _this.setState({
        messageNotificationPopup: ''
      });

      if (confirmResult) {
        var bannerInfo = _this._getParam(hasSubCategory);

        _this._updateBanner(bannerInfo);
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "_handlePressEnter", function (event) {
      if (event.charCode === 13) {
        event.preventDefault();

        _this._handleSubmit();
      }
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "resetToDefault", function () {
      var categoryList = _this.selectedAll(_this.state.categoryList);

      var subCategoryList = _this.selectedAll(_this.getSubCategories(categoryList));

      var currencyList = _this.selectedAll(_this.state.currencyList);

      var affTypeList = _this.selectedAll(_this.state.affTypeList);

      var deviceTypeList = _this.selectedAll(_this.state.deviceTypeList);

      _this.setState({
        categoryList: categoryList,
        subCategoryList: subCategoryList,
        currencyList: currencyList,
        affTypeList: affTypeList,
        deviceTypeList: deviceTypeList
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "getSubCategories", function (categories) {
      var _context16, _context17;

      var subCategoryList = _this.state.subCategoryList;

      var categoryNames = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context16 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(categories).call(categories, function (category) {
        return category.checked;
      })).call(_context16, function (category) {
        return category.categoryName;
      });

      var newSubCategoryList = _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_8___default()(_context17 = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(categoryNames).call(categoryNames, function (c) {
        var _context18;

        return _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(_context18 = _this.props.subCategoryList).call(_context18, function (sub) {
          return sub.parentCategoryName == c;
        });
      })).call(_context17, function (accumulator, field) {
        var newField = _babel_runtime_corejs3_core_js_stable_instance_reduce__WEBPACK_IMPORTED_MODULE_8___default()(field).call(field, function (acc, item) {
          var newItem = subCategoryList && _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(subCategoryList).call(subCategoryList, function (sub) {
            return sub.key === item.key;
          });

          return _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14___default()(acc).call(acc, !!newItem && !!newItem[0] ? newItem : item);
        }, []);

        return _babel_runtime_corejs3_core_js_stable_instance_concat__WEBPACK_IMPORTED_MODULE_14___default()(accumulator).call(accumulator, newField);
      }, []);

      return newSubCategoryList;
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "selectedAll", function (selectedArray) {
      return _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(selectedArray).call(selectedArray, function (item) {
        item.checked = true;
        return item;
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleChangeMultipleSelected", function (initialList, selectedKey, isCategory) {
      var _context19;

      var selectedList = typeof selectedKey === "string" ? _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(_context19 = selectedKey.split(',')).call(_context19, function (p) {
        return !!p;
      }) : selectedKey;
      return initialList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(initialList).call(initialList, function (item) {
        var key = item.key,
            name = item.name,
            categoryName = item.categoryName;
        var checked = _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9___default()(selectedList).call(selectedList, key.toString()) !== -1;
        return isCategory ? {
          categoryName: categoryName,
          key: key,
          name: name,
          checked: checked
        } : {
          key: key,
          name: name,
          checked: checked
        };
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleChangeCategory", function (item, selectedKey) {
      var categoryList = _this.props.categoryList;

      var newCategoryList = _this.handleChangeMultipleSelected(categoryList, selectedKey, true);

      var newSubCategoryList = _this.getSubCategories(newCategoryList);

      _this.setState({
        categoryList: newCategoryList,
        subCategoryList: newSubCategoryList
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleChangeSubCategory", function (item, selectedKey) {
      var subCategoryList = _this.state.subCategoryList;

      _this.setState({
        subCategoryList: _this.handleChangeMultipleSelected(subCategoryList, selectedKey)
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleChangeCurrency", function (item, selectedKey) {
      var currencyList = _this.state.currencyList;

      _this.setState({
        currencyList: _this.handleChangeMultipleSelected(currencyList, selectedKey)
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleChangeAffType", function (item, selectedKey) {
      var affTypeList = _this.state.affTypeList;

      _this.setState({
        affTypeList: _this.handleChangeMultipleSelected(affTypeList, selectedKey)
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_28___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_24___default()(_this), "handleChangeDevice", function (item, selectedKey) {
      var deviceTypeList = _this.state.deviceTypeList;

      _this.setState({
        deviceTypeList: _this.handleChangeMultipleSelected(deviceTypeList, selectedKey)
      });
    });

    var _this$props2 = _this.props,
        _categoryList = _this$props2.categoryList,
        _currencyList = _this$props2.currencyList,
        _affTypeList = _this$props2.affTypeList,
        _deviceTypeList = _this$props2.deviceTypeList;
    _this.state = _objectSpread(_objectSpread({}, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["defaultState"]), {}, {
      categoryList: _categoryList,
      subCategoryList: null,
      currencyList: _currencyList,
      affTypeList: _affTypeList,
      deviceTypeList: _deviceTypeList
    });
    _this.modalRef = /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createRef();
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_23___default()(EditBannerForm, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      this.resetToDefault();
    }
  }, {
    key: "componentDidUpdate",
    value: function componentDidUpdate(prevProps) {
      var selectedBannerId = this.props.selectedBannerId;

      if (prevProps.selectedBannerId !== selectedBannerId) {
        this.setState({
          isInsert: selectedBannerId === _common_libs_constants__WEBPACK_IMPORTED_MODULE_35__["defaultInsertId"]
        });

        if (selectedBannerId) {
          this._handleGetBannerDetail(selectedBannerId);
        } else {
          this.resetToDefault();
        }
      }
    }
  }, {
    key: "render",
    value: function render() {
      var _context20,
          _this2 = this;

      var _this$props3 = this.props,
          selectedBannerId = _this$props3.selectedBannerId,
          languageList = _this$props3.languageList;
      var _this$state5 = this.state,
          inputFileNames = _this$state5.inputFileNames,
          language = _this$state5.language,
          isShowMessage = _this$state5.isShowMessage,
          isSuccess = _this$state5.isSuccess,
          message = _this$state5.message,
          isInsert = _this$state5.isInsert,
          isDisabled = _this$state5.isDisabled,
          messageNotificationPopup = _this$state5.messageNotificationPopup,
          categoryList = _this$state5.categoryList,
          subCategoryList = _this$state5.subCategoryList,
          currencyList = _this$state5.currencyList,
          affTypeList = _this$state5.affTypeList,
          deviceTypeList = _this$state5.deviceTypeList,
          files = _this$state5.files;
      var isShowPopup = selectedBannerId !== null;
      var title = isInsert ? "Add Banner" : "Update Banner";
      var buttonLabel = isInsert ? "Upload" : "Update";

      var selectedCategory = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(_context20 = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_19___default()(categoryList).call(categoryList, function (item) {
        return item.checked;
      })).call(_context20, function (item) {
        return item.key;
      });

      var hasSubCategory = _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9___default()(selectedCategory).call(selectedCategory, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["Casino"]) !== -1 || _babel_runtime_corejs3_core_js_stable_instance_index_of__WEBPACK_IMPORTED_MODULE_9___default()(selectedCategory).call(selectedCategory, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["Sportsbook"]) !== -1;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_29___default.a.Fragment, null, !!messageNotificationPopup && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_common_components_NotificationPopup__WEBPACK_IMPORTED_MODULE_37__["default"], {
        message: messageNotificationPopup,
        isConfirmPopup: true,
        onClose: function onClose(confirmResult) {
          return _this2.handleCloseNotificationPopup(confirmResult, hasSubCategory);
        }
      }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Modal"], {
        isOpen: isShowPopup,
        toggle: this._handleClosePopup,
        backdrop: "static",
        innerRef: this.modalRef
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["ModalHeader"], {
        toggle: this._handleClosePopup
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("span", {
        className: "iconpopup"
      }), title), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["ModalBody"], null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("div", {
        className: "modal-body-wrapper"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("div", {
        className: "banner-list-form-container"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Alert"], {
        color: isSuccess ? 'success' : 'danger',
        isOpen: isShowMessage
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("div", {
        dangerouslySetInnerHTML: {
          __html: message
        }
      })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Form"], {
        tag: "form",
        className: "mb-2 p-1 border",
        autocomplete: "off"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], null, "[Valid Types: .gif, .jpg, .png, .jpeg]"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        className: "d-block"
      }, "[File Size Limit: 3MB]"), isInsert && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        className: "d-block"
      }, "[Maximum files: 10]"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("br", null), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "file",
        className: isInsert ? "required" : ""
      }, "File"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 9
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_common_components_InputFile__WEBPACK_IMPORTED_MODULE_40__["default"], {
        id: "file",
        className: "border ".concat(isInsert && isDisabled || files.length === 0 ? "text-danger" : "text-success"),
        isMultiple: isInsert,
        files: files,
        handleOnChangeFiles: this._handleFile
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "category",
        className: "required"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["headerText"].Category), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_components_multiple_select__WEBPACK_IMPORTED_MODULE_39__["default"], {
        dataSource: categoryList,
        keyField: "key",
        valueField: "name",
        statusField: "checked",
        maxDisplayItemCount: 1,
        noneSelectedLabel: "Please Select",
        onChange: this.handleChangeCategory
      }))), hasSubCategory && subCategoryList && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "subCategory",
        className: "required"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["headerText"].SubCategory), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_components_multiple_select__WEBPACK_IMPORTED_MODULE_39__["default"], {
        dataSource: subCategoryList,
        keyField: "key",
        valueField: "name",
        statusField: "checked",
        maxDisplayItemCount: 1,
        noneSelectedLabel: "Please Select",
        onChange: this.handleChangeSubCategory
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "language"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["headerText"].Language), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Input"], {
        type: "select",
        name: "language",
        id: "language",
        value: language,
        onChange: this._handleChange
      }, languageList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(languageList).call(languageList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("option", {
          key: "language-".concat(item.LanguageName),
          value: item.LanguageName
        }, item.LanguageName);
      })))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "currency",
        className: "required"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["headerText"].Currency), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_components_multiple_select__WEBPACK_IMPORTED_MODULE_39__["default"], {
        dataSource: currencyList,
        keyField: "key",
        valueField: "name",
        statusField: "checked",
        maxDisplayItemCount: 1,
        noneSelectedLabel: "Please Select",
        onChange: this.handleChangeCurrency
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "affType",
        className: "required"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["headerText"].AffType), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_components_multiple_select__WEBPACK_IMPORTED_MODULE_39__["default"], {
        dataSource: affTypeList,
        keyField: "key",
        valueField: "name",
        statusField: "checked",
        maxDisplayItemCount: 1,
        noneSelectedLabel: "Please Select",
        onChange: this.handleChangeAffType
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "deviceType",
        className: "required"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["headerText"].Device), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 5
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(_components_multiple_select__WEBPACK_IMPORTED_MODULE_39__["default"], {
        dataSource: deviceTypeList,
        keyField: "key",
        valueField: "name",
        statusField: "checked",
        maxDisplayItemCount: 1,
        noneSelectedLabel: "Please Select",
        onChange: this.handleChangeDevice
      }))), isInsert ? inputFileNames && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_12___default()(inputFileNames).call(inputFileNames, function (value, index) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
          row: true,
          className: "mb-2",
          key: "FileName".concat(index + 1)
        }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
          sm: 3,
          for: "inputFileName"
        }, "File Name ".concat(index + 1)), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
          sm: 7
        }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Input"], {
          type: "text",
          name: "inputFileName",
          id: "inputFileName".concat(index),
          value: value,
          onChange: function onChange(e) {
            return _this2._handleChangeInputFileName(e, index);
          },
          onKeyPress: _this2._handlePressEnter
        })), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement("div", null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Button"], {
          className: "mr-3",
          type: "button",
          color: "danger",
          for: "inputFileName",
          disabled: files && !files.length,
          onClick: function onClick() {
            return _this2._handleRemoveImageAt(index);
          }
        }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["globalResource"].Remove)));
      }) : /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["FormGroup"], {
        row: true,
        className: "mb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Label"], {
        sm: 3,
        for: "inputFileName"
      }, "File Name"), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Col"], {
        sm: 7
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Input"], {
        type: "text",
        name: "inputFileName",
        id: "inputFileName",
        value: inputFileNames && inputFileNames[0],
        onChange: function onChange(e) {
          return _this2._handleChangeInputFileName(e, 0);
        },
        onKeyPress: this._handlePressEnter
      })))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Button"], {
        type: "button",
        color: "success",
        className: "mr-3",
        disabled: isInsert || inputFileNames && !inputFileNames[0] ? isDisabled : false,
        onClick: function onClick() {
          return _this2._handleSubmit(hasSubCategory);
        }
      }, buttonLabel), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_29___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_32__["Button"], {
        type: "button",
        color: "info",
        onClick: this._handleClosePopup
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_34__["globalResource"].Cancel))))));
    }
  }]);

  return EditBannerForm;
}(react__WEBPACK_IMPORTED_MODULE_29__["PureComponent"]);

function mapStateToProps(_ref4) {
  var selectedBannerId = _ref4.selectedBannerId,
      param = _ref4.param,
      categoryList = _ref4.categoryList,
      subCategoryCasinoList = _ref4.subCategoryCasinoList,
      subCategorySportsbookList = _ref4.subCategorySportsbookList,
      languageList = _ref4.languageList,
      currencyList = _ref4.currencyList,
      sizeList = _ref4.sizeList,
      affTypeList = _ref4.affTypeList,
      deviceTypeList = _ref4.deviceTypeList,
      subCategoryList = _ref4.subCategoryList;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_11___default()({}, {
    selectedBannerId: selectedBannerId,
    param: param,
    categoryList: categoryList,
    subCategoryCasinoList: subCategoryCasinoList,
    subCategorySportsbookList: subCategorySportsbookList,
    languageList: languageList,
    currencyList: currencyList,
    sizeList: sizeList,
    affTypeList: affTypeList,
    deviceTypeList: deviceTypeList,
    subCategoryList: subCategoryList
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_31__["bindActionCreators"])({
    setSelectedBannerId: _actions_index__WEBPACK_IMPORTED_MODULE_33__["setSelectedBannerId"],
    getBannerDetail: _actions_index__WEBPACK_IMPORTED_MODULE_33__["getBannerDetail"],
    fetchData: _actions_index__WEBPACK_IMPORTED_MODULE_33__["fetchData"],
    insertBanner: _actions_index__WEBPACK_IMPORTED_MODULE_33__["insertBanner"],
    updateBanner: _actions_index__WEBPACK_IMPORTED_MODULE_33__["updateBanner"],
    toggleLoader: _actions_index__WEBPACK_IMPORTED_MODULE_33__["toggleLoader"],
    getSizeList: _actions_index__WEBPACK_IMPORTED_MODULE_33__["getSizeList"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_30__["connect"])(mapStateToProps, mapDispatchToProps)(EditBannerForm));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/Filter.jsx":
/*!******************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/Filter.jsx ***!
  \******************************************************************************************/
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
/* harmony import */ var _actions__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../actions */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/index.js");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js");












function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_9___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_8___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }







var Filter = /*#__PURE__*/function (_PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_7___default()(Filter, _PureComponent);

  var _super = _createSuper(Filter);

  function Filter(props) {
    var _this;

    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_4___default()(this, Filter);

    _this = _super.call(this, props);

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_handleChangeCategory", function (event) {
      var selectedCategory = event.target.value;

      var param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, _this.state, {
        category: selectedCategory,
        subCategory: ''
      });

      _this.props.getSizeList(param);

      _this.setState({
        category: selectedCategory,
        subCategory: ''
      });
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_handleChange", function (event) {
      var name = event.target.name;
      var value = event.target.value;

      var param = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, _this.state, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, name, value));

      _this.props.getSizeList(param);

      _this.setState(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()({}, name, value));
    });

    _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_10___default()(_babel_runtime_corejs3_helpers_assertThisInitialized__WEBPACK_IMPORTED_MODULE_6___default()(_this), "_handleSearch", function () {
      var pagingOption = _this.props.pagingOption;

      var newParam = _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, _this.state, {
        PageIndex: 1,
        PageSize: pagingOption.PageSize
      });

      _this.props.setParam(newParam);
    });

    var paramFromHome = _this.props.paramFromHome;
    _this.state = {
      category: '',
      subCategory: '',
      language: '',
      currency: '',
      imageType: paramFromHome && paramFromHome.ImageType ? paramFromHome.ImageType : '',
      affType: _libs_constants__WEBPACK_IMPORTED_MODULE_16__["Asian"],
      deviceType: _libs_constants__WEBPACK_IMPORTED_MODULE_16__["deviceTypes"][_libs_constants__WEBPACK_IMPORTED_MODULE_16__["pcLaptop"]],
      size: ''
    };
    return _this;
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_5___default()(Filter, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      var paramFromHome = this.props.paramFromHome;

      if (paramFromHome && paramFromHome.IsFromHome) {
        this._handleSearch();

        this.props.setParamFromHome({
          IsFromHome: false
        });
      }
    }
  }, {
    key: "render",
    value: function render() {
      var _context,
          _this2 = this;

      var _this$props = this.props,
          categoryList = _this$props.categoryList,
          subCategoryCasinoList = _this$props.subCategoryCasinoList,
          subCategorySportsbookList = _this$props.subCategorySportsbookList,
          languageList = _this$props.languageList,
          currencyList = _this$props.currencyList,
          imageTypeList = _this$props.imageTypeList,
          sizeList = _this$props.sizeList,
          affTypeList = _this$props.affTypeList;
      var _this$state = this.state,
          category = _this$state.category,
          subCategory = _this$state.subCategory,
          language = _this$state.language,
          currency = _this$state.currency,
          imageType = _this$state.imageType,
          affType = _this$state.affType,
          deviceType = _this$state.deviceType,
          size = _this$state.size;
      var isCategoryCasino = category === _libs_constants__WEBPACK_IMPORTED_MODULE_16__["Casino"];
      var hasSubCategory = isCategoryCasino || category === _libs_constants__WEBPACK_IMPORTED_MODULE_16__["Sportsbook"];
      var subCategoryList = hasSubCategory ? isCategoryCasino ? subCategoryCasinoList : subCategorySportsbookList : null;
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("div", {
        className: "filter-container my-2 pb-0"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Form"], {
        inline: true,
        tag: "div"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "category",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Category), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "category",
        id: "category",
        value: category,
        onChange: this._handleChangeCategory
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'category-all',
        value: ''
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), categoryList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(categoryList).call(categoryList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "category-".concat(item.name),
          value: item.name
        }, item.name);
      }))), subCategoryList && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "subCategory",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].SubCategory), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "subCategory",
        id: "subCategory",
        value: subCategory,
        onChange: this._handleChange
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'subCategory-all',
        value: ''
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(subCategoryList).call(subCategoryList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "subCategory-".concat(item.SubCategoryName),
          value: item.SubCategoryName
        }, item.SubCategoryName);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "language",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Language), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "language",
        id: "language",
        value: language,
        onChange: this._handleChange
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'language-all',
        value: ''
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), languageList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(languageList).call(languageList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "language-".concat(item.LanguageName),
          value: item.LanguageName
        }, item.LanguageName);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "currency",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Currency), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "currency",
        id: "currency",
        value: currency,
        onChange: this._handleChange
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'currency-all',
        value: ''
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), currencyList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(currencyList).call(currencyList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "currency-".concat(item.name),
          value: item.name
        }, item.name);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "imageType",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Type), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "imageType",
        id: "imageType",
        value: imageType,
        onChange: this._handleChange
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'imageType-all',
        value: ''
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), imageTypeList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(imageTypeList).call(imageTypeList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "imageType-".concat(item.Name),
          value: item.Name
        }, item.Name);
      }))), affTypeList && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "affType",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].AffType), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "affType",
        id: "affType",
        value: affType,
        onChange: this._handleChange
      }, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(affTypeList).call(affTypeList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "affType-".concat(item.key),
          value: item.key
        }, item.name);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "deviceType",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Device), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "deviceType",
        id: "deviceType",
        value: deviceType,
        onChange: this._handleChange
      }, _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(_context = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_1___default()(_libs_constants__WEBPACK_IMPORTED_MODULE_16__["deviceTypes"])).call(_context, function (key) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "deviceType-".concat(key),
          value: _libs_constants__WEBPACK_IMPORTED_MODULE_16__["deviceTypes"][key]
        }, key);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["FormGroup"], {
        className: "mr-sm-3 pb-2"
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Label"], {
        for: "size",
        className: "mr-sm-2"
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["headerText"].Size), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Input"], {
        type: "select",
        name: "size",
        id: "size",
        value: size,
        onChange: function onChange(e) {
          return _this2.setState({
            size: e.target.value
          });
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
        key: 'size-all',
        value: ''
      }, _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].All), sizeList && _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_2___default()(sizeList).call(sizeList, function (item) {
        return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("option", {
          key: "size-".concat(item.name),
          value: item.name
        }, item.name);
      }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_14__["Button"], {
        outline: true,
        color: "primary",
        type: "button",
        className: "mb-2",
        onClick: this._handleSearch
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_11___default.a.createElement("span", {
        className: "oi oi-magnifying-glass"
      }), " ", _libs_constants__WEBPACK_IMPORTED_MODULE_16__["globalResource"].Search)));
    }
  }]);

  return Filter;
}(react__WEBPACK_IMPORTED_MODULE_11__["PureComponent"]);

function mapStateToProps(_ref) {
  var categoryList = _ref.categoryList,
      subCategoryCasinoList = _ref.subCategoryCasinoList,
      subCategorySportsbookList = _ref.subCategorySportsbookList,
      languageList = _ref.languageList,
      currencyList = _ref.currencyList,
      imageTypeList = _ref.imageTypeList,
      sizeList = _ref.sizeList,
      affTypeList = _ref.affTypeList,
      pagingOption = _ref.pagingOption,
      paramFromHome = _ref.paramFromHome;
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_3___default()({}, {
    categoryList: categoryList,
    subCategoryCasinoList: subCategoryCasinoList,
    subCategorySportsbookList: subCategorySportsbookList,
    languageList: languageList,
    currencyList: currencyList,
    imageTypeList: imageTypeList,
    sizeList: sizeList,
    affTypeList: affTypeList,
    pagingOption: pagingOption,
    paramFromHome: paramFromHome
  });
}

function mapDispatchToProps(dispatch) {
  return Object(redux__WEBPACK_IMPORTED_MODULE_13__["bindActionCreators"])({
    setParam: _actions__WEBPACK_IMPORTED_MODULE_15__["setParam"],
    setParamFromHome: _actions__WEBPACK_IMPORTED_MODULE_15__["setParamFromHome"],
    getSizeList: _actions__WEBPACK_IMPORTED_MODULE_15__["getSizeList"]
  }, dispatch);
}

/* harmony default export */ __webpack_exports__["default"] = (Object(react_redux__WEBPACK_IMPORTED_MODULE_12__["connect"])(mapStateToProps, mapDispatchToProps)(Filter));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/index.jsx":
/*!******************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/index.jsx ***!
  \******************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "default", function() { return App; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/map */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/map.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_7___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_7__);
/* harmony import */ var react_redux__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! react-redux */ "./node_modules/react-redux/es/index.js");
/* harmony import */ var _components_AppContainer__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./components/AppContainer */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/components/AppContainer.jsx");
/* harmony import */ var _reducers__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./reducers */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/reducers/index.js");
/* harmony import */ var _modules_store__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../../../modules/store */ "./Areas/Portal/www/source/js/modules/store.js");
/* harmony import */ var _modules_claim__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../../../modules/claim */ "./Areas/Portal/www/source/js/modules/claim.js");








function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }







var _window$initialState = window.initialState,
    categoryList = _window$initialState.categoryList,
    subCategoryList = _window$initialState.subCategoryList,
    languageList = _window$initialState.languageList,
    currencyList = _window$initialState.currencyList,
    imageTypeList = _window$initialState.imageTypeList,
    sizeList = _window$initialState.sizeList,
    affTypeList = _window$initialState.affTypeList,
    paramFromHome = _window$initialState.paramFromHome,
    subCategoryCasinoList = _window$initialState.subCategoryCasinoList,
    subCategorySportsbookList = _window$initialState.subCategorySportsbookList,
    bannerRootUrl = _window$initialState.bannerRootUrl;

var newCategoryList = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default()(categoryList).call(categoryList, function (item) {
  return {
    categoryName: item.CategoryName,
    key: item.CategoryName,
    name: item.CategoryName,
    checked: false
  };
});

var newSubCategoryList = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default()(subCategoryList).call(subCategoryList, function (item) {
  return {
    key: item.SubCategoryName,
    name: item.SubCategoryName,
    parentCategoryName: item.CategoryName,
    checked: false
  };
});

var newCurrencyList = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default()(currencyList).call(currencyList, function (item) {
  return {
    key: item.Name,
    name: item.Name,
    checked: false
  };
});

var newSizeList = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default()(sizeList).call(sizeList, function (item) {
  return {
    key: item.Name,
    name: item.Name,
    checked: false
  };
});

var newAffTypeList = _babel_runtime_corejs3_core_js_stable_instance_map__WEBPACK_IMPORTED_MODULE_6___default()(affTypeList).call(affTypeList, function (item) {
  return {
    key: item.AffTypeId,
    name: item.AffTypeName,
    checked: false
  };
});

var App = /*#__PURE__*/function (_React$PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3___default()(App, _React$PureComponent);

  var _super = _createSuper(App);

  function App(props) {
    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default()(this, App);

    return _super.call(this, props);
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default()(App, [{
    key: "render",
    value: function render() {
      var claimsPrincipal = _modules_claim__WEBPACK_IMPORTED_MODULE_12__["default"].currentClaimPrincipal;
      var store = Object(_modules_store__WEBPACK_IMPORTED_MODULE_11__["configureStore"])({
        languageList: languageList,
        paramFromHome: paramFromHome,
        categoryList: newCategoryList,
        subCategoryList: newSubCategoryList,
        currencyList: newCurrencyList,
        imageTypeList: imageTypeList,
        sizeList: newSizeList,
        affTypeList: newAffTypeList,
        subCategoryCasinoList: subCategoryCasinoList,
        subCategorySportsbookList: subCategorySportsbookList,
        bannerRootUrl: bannerRootUrl,
        claimsPrincipal: claimsPrincipal
      }, _reducers__WEBPACK_IMPORTED_MODULE_10__["default"]);
      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(react_redux__WEBPACK_IMPORTED_MODULE_8__["Provider"], {
        store: store
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_7___default.a.createElement(_components_AppContainer__WEBPACK_IMPORTED_MODULE_9__["default"], null));
    }
  }]);

  return App;
}(react__WEBPACK_IMPORTED_MODULE_7___default.a.PureComponent);



/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js":
/*!**************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js ***!
  \**************************************************************************************/
/*! exports provided: deviceTypes, globalResource, pcLaptop, mobile, Casino, Sportsbook, Asian, maxWidthHeight, maxLength, regSpecialCharacters, headerText, messages, defaultState */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Casino", function() { return Casino; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Sportsbook", function() { return Sportsbook; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Asian", function() { return Asian; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "maxWidthHeight", function() { return maxWidthHeight; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "maxLength", function() { return maxLength; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "regSpecialCharacters", function() { return regSpecialCharacters; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "headerText", function() { return headerText; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "messages", function() { return messages; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "defaultState", function() { return defaultState; });
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../../../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "deviceTypes", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_0__["deviceTypes"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "pcLaptop", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_0__["pcLaptop"]; });

/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "mobile", function() { return _modules_constants__WEBPACK_IMPORTED_MODULE_0__["mobile"]; });

/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony reexport (safe) */ __webpack_require__.d(__webpack_exports__, "globalResource", function() { return _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"]; });




var Casino = 'Casino';
var Sportsbook = 'Sportsbook';
var defaultLanguage = 'Chinese Simplified';
var Asian = 2;
var maxWidthHeight = 501;
var maxLength = 50;
var regSpecialCharacters = '[\\\\\/\:\*\?\"\<\>\|\#\%\&\+]';
var headerText = {
  Index: _modules_globalResource__WEBPACK_IMPORTED_MODULE_1__["globalResource"].Index,
  Images: 'Images',
  FileName: 'File Name',
  Category: 'Category',
  SubCategory: 'Sub Category',
  Language: 'Language',
  Currency: 'Currency',
  Type: 'Type',
  Size: 'Size',
  AffType: 'Affiliate Type',
  Device: 'Device',
  ImageType: 'Image Type'
};
var messages = {
  PleaseSelect: 'Please select banner(s) to delete',
  AreYouSureDeleteItems: 'Are you sure to delete the selected banner(s)?',
  DeleteItemsSuccessfully: 'Delete banner(s) Successfully'
};
var defaultState = {
  files: [],
  inputFileNames: [''],
  language: defaultLanguage,
  isInsert: true,
  isShowMessage: false,
  isSuccess: true,
  message: '',
  isDisabled: true,
  messageNotificationPopup: ''
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/utils.js":
/*!**********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/utils.js ***!
  \**********************************************************************************/
/*! exports provided: validateFileExtension, validateInputFile, resizeWidthHeight */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "validateFileExtension", function() { return validateFileExtension; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "validateInputFile", function() { return validateInputFile; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "resizeWidthHeight", function() { return resizeWidthHeight; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/instance/last-index-of */ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/last-index-of.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _constants__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./constants */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/libs/constants.js");


var validateFileExtension = function validateFileExtension(fileName) {
  var result = {
    isOK: false,
    message: "Sorry! We just support these file types : '.jpg', '.gif' , '.png','.jpeg'"
  };

  if (!!fileName) {
    var fileExtension = fileName.substring(_babel_runtime_corejs3_core_js_stable_instance_last_index_of__WEBPACK_IMPORTED_MODULE_0___default()(fileName).call(fileName, '.') + 1, fileName.length).toLowerCase();
    var isOK = fileExtension === "jpg" || fileExtension === "jpeg" || fileExtension === "png" || fileExtension === "gif";

    if (isOK) {
      result.isOK = isOK;
      result.message = '';
    }
  }

  return result;
};
var validateInputFile = function validateInputFile(inputFileName) {
  var result = validateFileExtension(inputFileName);

  if (!!inputFileName && inputFileName.length > _constants__WEBPACK_IMPORTED_MODULE_1__["maxLength"]) {
    result.isOK = false;
    result.message = 'The maximum length of file name is 50. Please choose another name.';
  }

  return result;
};
var resizeWidthHeight = function resizeWidthHeight(realWidth, realHeight) {
  var size = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : 500;
  var width = Number(realWidth);
  var height = Number(realHeight);

  if (width < size && height < size) {
    return {
      width: width,
      height: height
    };
  }

  if (width > height) {
    height = size * height / width;
    width = size;
  } else {
    width = size * width / height;
    height = size;
  }

  return {
    width: width,
    height: height
  };
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/reducers/index.js":
/*!**************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/banner-list/reducers/index.js ***!
  \**************************************************************************************/
/*! exports provided: categoryList, subCategoryList, languageList, currencyList, imageTypeList, affTypeList, deviceTypeList, bannerRootUrl, subCategoryCasinoList, subCategorySportsbookList, sizeList, param, paramFromHome, bannerDetailUrl, selectedBannerId, checkedBanners, shouldResetScrollPosition, gridData, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "categoryList", function() { return categoryList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "subCategoryList", function() { return subCategoryList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "languageList", function() { return languageList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "currencyList", function() { return currencyList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "imageTypeList", function() { return imageTypeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "affTypeList", function() { return affTypeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "deviceTypeList", function() { return deviceTypeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "bannerRootUrl", function() { return bannerRootUrl; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "subCategoryCasinoList", function() { return subCategoryCasinoList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "subCategorySportsbookList", function() { return subCategorySportsbookList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "sizeList", function() { return sizeList; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "param", function() { return param; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "paramFromHome", function() { return paramFromHome; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "bannerDetailUrl", function() { return bannerDetailUrl; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "selectedBannerId", function() { return selectedBannerId; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "checkedBanners", function() { return checkedBanners; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "shouldResetScrollPosition", function() { return shouldResetScrollPosition; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "gridData", function() { return gridData; });
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
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_8___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_8__);
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9__);
/* harmony import */ var redux__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! redux */ "./node_modules/redux/es/redux.js");
/* harmony import */ var _modules_createReducer__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ../../../../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");
/* harmony import */ var _reducers_pagingReducer__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ../../../../reducers/pagingReducer */ "./Areas/Portal/www/source/js/reducers/pagingReducer.js");
/* harmony import */ var _reducers_loaderReducer__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ../../../../reducers/loaderReducer */ "./Areas/Portal/www/source/js/reducers/loaderReducer.js");
/* harmony import */ var _reducers_claimReducer__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ../../../../reducers/claimReducer */ "./Areas/Portal/www/source/js/reducers/claimReducer.js");
/* harmony import */ var _actions_types__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ../actions/types */ "./Areas/Portal/www/source/js/pages/media-gallery/banner-list/actions/types.js");











function ownKeys(object, enumerableOnly) { var keys = _babel_runtime_corejs3_core_js_stable_object_keys__WEBPACK_IMPORTED_MODULE_7___default()(object); if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default.a) { var symbols = _babel_runtime_corejs3_core_js_stable_object_get_own_property_symbols__WEBPACK_IMPORTED_MODULE_6___default()(object); if (enumerableOnly) symbols = _babel_runtime_corejs3_core_js_stable_instance_filter__WEBPACK_IMPORTED_MODULE_5___default()(symbols).call(symbols, function (sym) { return _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(object, sym).enumerable; }); keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i] != null ? arguments[i] : {}; if (i % 2) { var _context; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context = ownKeys(Object(source), true)).call(_context, function (key) { _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()(target, key, source[key]); }); } else if (_babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default.a) { _babel_runtime_corejs3_core_js_stable_object_define_properties__WEBPACK_IMPORTED_MODULE_1___default()(target, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptors__WEBPACK_IMPORTED_MODULE_2___default()(source)); } else { var _context2; _babel_runtime_corejs3_core_js_stable_instance_for_each__WEBPACK_IMPORTED_MODULE_3___default()(_context2 = ownKeys(Object(source))).call(_context2, function (key) { _babel_runtime_corejs3_core_js_stable_object_define_property__WEBPACK_IMPORTED_MODULE_0___default()(target, key, _babel_runtime_corejs3_core_js_stable_object_get_own_property_descriptor__WEBPACK_IMPORTED_MODULE_4___default()(source, key)); }); } } return target; }







var categoryList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var subCategoryList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var languageList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var currencyList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var imageTypeList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var affTypeList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var deviceTypeList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])([{
  key: 0,
  name: "PC / Laptop",
  checked: false
}, {
  key: 1,
  name: "Mobile",
  checked: false
}], {});
var bannerRootUrl = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var subCategoryCasinoList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var subCategorySportsbookList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, {});
var sizeList = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_SIZE_LIST"], function (prevState, action) {
  return action.data;
}));
var param = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_PARAM"], function (prevState, action) {
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_8___default()({}, prevState, action.data);
}));
var paramFromHome = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_PARAM_FROM_HOME"], function (prevState, action) {
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_8___default()({}, prevState, action.data);
}));
var bannerDetailUrl = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_BANNER_DETAIL_URL"], function (prevState, action) {
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_8___default()({}, _objectSpread({}, action.data));
}));
var selectedBannerId = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_SELECTED_BANNER_ID"], function (prevState, action) {
  return action.data;
}));
var checkedBanners = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])({}, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_CHECKED_BANNERS"], function (prevState, action) {
  return action.data;
}));
var shouldResetScrollPosition = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(true, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SHOULD_RESET_SCROLL_POSITION"], function (prevState, action) {
  return action.data;
}));
var gridData = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_11__["default"])(null, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_9___default()({}, _actions_types__WEBPACK_IMPORTED_MODULE_15__["SET_GRID_DATA"], function (prevState, action) {
  return action.data;
}));
/* harmony default export */ __webpack_exports__["default"] = (Object(redux__WEBPACK_IMPORTED_MODULE_10__["combineReducers"])({
  categoryList: categoryList,
  subCategoryList: subCategoryList,
  subCategoryCasinoList: subCategoryCasinoList,
  subCategorySportsbookList: subCategorySportsbookList,
  languageList: languageList,
  currencyList: currencyList,
  imageTypeList: imageTypeList,
  sizeList: sizeList,
  affTypeList: affTypeList,
  deviceTypeList: deviceTypeList,
  bannerRootUrl: bannerRootUrl,
  param: param,
  paramFromHome: paramFromHome,
  gridData: gridData,
  pagingOption: _reducers_pagingReducer__WEBPACK_IMPORTED_MODULE_12__["pagingOption"],
  isShowLoader: _reducers_loaderReducer__WEBPACK_IMPORTED_MODULE_13__["isShowLoader"],
  checkedBanners: checkedBanners,
  bannerDetailUrl: bannerDetailUrl,
  selectedBannerId: selectedBannerId,
  shouldResetScrollPosition: shouldResetScrollPosition,
  claimsPrincipal: _reducers_claimReducer__WEBPACK_IMPORTED_MODULE_14__["claimsPrincipal"]
}));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/common/components/ControlButtons.jsx":
/*!*********************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/common/components/ControlButtons.jsx ***!
  \*********************************************************************************************/
/*! exports provided: ControlButtons, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ControlButtons", function() { return ControlButtons; });
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _common_components_SelectAll__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../common/components/SelectAll */ "./Areas/Portal/www/source/js/pages/common/components/SelectAll.jsx");
/* harmony import */ var _common_components_ButtonDeleteItems__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../../common/components/ButtonDeleteItems */ "./Areas/Portal/www/source/js/pages/common/components/ButtonDeleteItems.jsx");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/common/libs/constants.js");
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");






var ControlButtons = function ControlButtons(props) {
  function _handleAddItem() {
    props.setSelectedItemId(_libs_constants__WEBPACK_IMPORTED_MODULE_4__["defaultInsertId"]);
    props.setShouldResetScrollPosition(false);
  }

  var hasData = props.gridData && !!props.gridData.length;
  return props.gridData && /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_0___default.a.Fragment, null, hasData ? /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_0___default.a.Fragment, null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    className: "my-2"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_common_components_ButtonDeleteItems__WEBPACK_IMPORTED_MODULE_3__["default"], {
    checkedItems: props.checkedItems,
    toggleLoader: props.toggleLoader,
    deleteItems: props.deleteItems,
    fetchData: function fetchData() {
      return props.fetchData(props.param);
    },
    messages: props.messages,
    hasIcon: true,
    isOpenNotification: true
  }), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Button"], {
    outline: true,
    type: "button",
    className: "mx-2",
    onClick: _handleAddItem
  }, _modules_globalResource__WEBPACK_IMPORTED_MODULE_5__["globalResource"].Add, " ", /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("span", {
    className: "oi oi-plus ml-2"
  }))), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(_common_components_SelectAll__WEBPACK_IMPORTED_MODULE_2__["default"], {
    checkedList: props.checkedItems,
    setCheckedList: props.setCheckedItems,
    setShouldResetScrollPosition: props.setShouldResetScrollPosition
  })) : /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("div", {
    className: "my-2"
  }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_1__["Button"], {
    outline: true,
    type: "button",
    onClick: _handleAddItem
  }, _modules_globalResource__WEBPACK_IMPORTED_MODULE_5__["globalResource"].Add, " ", /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement("span", {
    className: "oi oi-plus ml-2"
  }))));
};
/* harmony default export */ __webpack_exports__["default"] = (/*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_0___default.a.memo(ControlButtons));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/common/components/InputFile.jsx":
/*!****************************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/common/components/InputFile.jsx ***!
  \****************************************************************************************/
/*! exports provided: InputFile, default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "InputFile", function() { return InputFile; });
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/reflect/construct */ "./node_modules/@babel/runtime-corejs3/core-js-stable/reflect/construct.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/classCallCheck */ "./node_modules/@babel/runtime-corejs3/helpers/classCallCheck.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/createClass */ "./node_modules/@babel/runtime-corejs3/helpers/createClass.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2__);
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/inherits */ "./node_modules/@babel/runtime-corejs3/helpers/inherits.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/possibleConstructorReturn */ "./node_modules/@babel/runtime-corejs3/helpers/possibleConstructorReturn.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/getPrototypeOf */ "./node_modules/@babel/runtime-corejs3/helpers/getPrototypeOf.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5__);
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! react */ "./node_modules/react/index.js");
/* harmony import */ var react__WEBPACK_IMPORTED_MODULE_6___default = /*#__PURE__*/__webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_6__);
/* harmony import */ var reactstrap__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! reactstrap */ "./node_modules/reactstrap/es/index.js");
/* harmony import */ var _modules_globalResource__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ../../../../modules/globalResource */ "./Areas/Portal/www/source/js/modules/globalResource.js");
/* harmony import */ var _libs_constants__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ../libs/constants */ "./Areas/Portal/www/source/js/pages/media-gallery/common/libs/constants.js");







function _createSuper(Derived) { var hasNativeReflectConstruct = _isNativeReflectConstruct(); return function _createSuperInternal() { var Super = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(Derived), result; if (hasNativeReflectConstruct) { var NewTarget = _babel_runtime_corejs3_helpers_getPrototypeOf__WEBPACK_IMPORTED_MODULE_5___default()(this).constructor; result = _babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Super, arguments, NewTarget); } else { result = Super.apply(this, arguments); } return _babel_runtime_corejs3_helpers_possibleConstructorReturn__WEBPACK_IMPORTED_MODULE_4___default()(this, result); }; }

function _isNativeReflectConstruct() { if (typeof Reflect === "undefined" || !_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a) return false; if (_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default.a.sham) return false; if (typeof Proxy === "function") return true; try { Date.prototype.toString.call(_babel_runtime_corejs3_core_js_stable_reflect_construct__WEBPACK_IMPORTED_MODULE_0___default()(Date, [], function () {})); return true; } catch (e) { return false; } }





var InputFile = /*#__PURE__*/function (_React$PureComponent) {
  _babel_runtime_corejs3_helpers_inherits__WEBPACK_IMPORTED_MODULE_3___default()(InputFile, _React$PureComponent);

  var _super = _createSuper(InputFile);

  function InputFile(props) {
    _babel_runtime_corejs3_helpers_classCallCheck__WEBPACK_IMPORTED_MODULE_1___default()(this, InputFile);

    return _super.call(this, props);
  }

  _babel_runtime_corejs3_helpers_createClass__WEBPACK_IMPORTED_MODULE_2___default()(InputFile, [{
    key: "componentDidMount",
    value: function componentDidMount() {
      var _this = this;

      this.$input = $(this.input);
      this.$input.css('cursor', 'pointer');
      this.$input.click(function () {
        _this.$input.children('input')[0].click();
      });
    }
  }, {
    key: "componentWillUnmount",
    value: function componentWillUnmount() {
      this.$input.unbind('click');
    }
  }, {
    key: "render",
    value: function render() {
      var _this2 = this;

      var _this$props = this.props,
          className = _this$props.className,
          id = _this$props.id,
          files = _this$props.files,
          isMultiple = _this$props.isMultiple,
          handleOnChangeFiles = _this$props.handleOnChangeFiles;
      var text = _libs_constants__WEBPACK_IMPORTED_MODULE_9__["messages"].NoFileChoosen;

      if (files && files.length === 1) {
        text = files[0].name;
      }

      if (files && files.length > 1) {
        text = "".concat(files.length, " files");
      }

      return /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.createElement(react__WEBPACK_IMPORTED_MODULE_6___default.a.Fragment, null, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.createElement("div", {
        className: className,
        ref: function ref(el) {
          return _this2.input = el;
        }
      }, /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_7__["Button"], {
        type: "button"
      }, _modules_globalResource__WEBPACK_IMPORTED_MODULE_8__["globalResource"].ChooseFile), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_7__["Label"], {
        className: "ml-1"
      }, text), /*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.createElement(reactstrap__WEBPACK_IMPORTED_MODULE_7__["Input"], {
        type: "file",
        name: "file",
        id: id,
        hidden: true,
        files: files,
        multiple: isMultiple,
        onChange: handleOnChangeFiles,
        accept: "image/jpg, image/gif, image/png, image/jpeg"
      })));
    }
  }]);

  return InputFile;
}(react__WEBPACK_IMPORTED_MODULE_6___default.a.PureComponent);
/* harmony default export */ __webpack_exports__["default"] = (/*#__PURE__*/react__WEBPACK_IMPORTED_MODULE_6___default.a.memo(InputFile));

/***/ }),

/***/ "./Areas/Portal/www/source/js/pages/media-gallery/common/libs/constants.js":
/*!*********************************************************************************!*\
  !*** ./Areas/Portal/www/source/js/pages/media-gallery/common/libs/constants.js ***!
  \*********************************************************************************/
/*! exports provided: defaultInsertId, messages */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "defaultInsertId", function() { return defaultInsertId; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "messages", function() { return messages; });
var defaultInsertId = 0;
var messages = {
  NoFileChoosen: 'No file choosen',
  Files: 'files'
};

/***/ }),

/***/ "./Areas/Portal/www/source/js/reducers/claimReducer.js":
/*!*************************************************************!*\
  !*** ./Areas/Portal/www/source/js/reducers/claimReducer.js ***!
  \*************************************************************/
/*! exports provided: claimsPrincipal */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "claimsPrincipal", function() { return claimsPrincipal; });
/* harmony import */ var _modules_createReducer__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");

var claimsPrincipal = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_0__["default"])({}, {});

/***/ }),

/***/ "./Areas/Portal/www/source/js/reducers/loaderReducer.js":
/*!**************************************************************!*\
  !*** ./Areas/Portal/www/source/js/reducers/loaderReducer.js ***!
  \**************************************************************/
/*! exports provided: isShowLoader */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "isShowLoader", function() { return isShowLoader; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _modules_createReducer__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");
/* harmony import */ var _actions_actionTypes__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../actions/actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");



var isShowLoader = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_1__["default"])(false, _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()({}, _actions_actionTypes__WEBPACK_IMPORTED_MODULE_2__["TOGGLE_LOADER"], function (prevState, action) {
  return action.data;
}));

/***/ }),

/***/ "./Areas/Portal/www/source/js/reducers/pagingReducer.js":
/*!**************************************************************!*\
  !*** ./Areas/Portal/www/source/js/reducers/pagingReducer.js ***!
  \**************************************************************/
/*! exports provided: pagingOption */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "pagingOption", function() { return pagingOption; });
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @babel/runtime-corejs3/helpers/defineProperty */ "./node_modules/@babel/runtime-corejs3/helpers/defineProperty.js");
/* harmony import */ var _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0__);
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @babel/runtime-corejs3/core-js-stable/object/assign */ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js");
/* harmony import */ var _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1__);
/* harmony import */ var _modules_createReducer__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../modules/createReducer */ "./Areas/Portal/www/source/js/modules/createReducer.js");
/* harmony import */ var _actions_actionTypes__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../actions/actionTypes */ "./Areas/Portal/www/source/js/actions/actionTypes.js");
/* harmony import */ var _modules_constants__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../modules/constants */ "./Areas/Portal/www/source/js/modules/constants.js");





var pagingOption = Object(_modules_createReducer__WEBPACK_IMPORTED_MODULE_2__["default"])(_babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, _modules_constants__WEBPACK_IMPORTED_MODULE_4__["defaultPagingOption"]), _babel_runtime_corejs3_helpers_defineProperty__WEBPACK_IMPORTED_MODULE_0___default()({}, _actions_actionTypes__WEBPACK_IMPORTED_MODULE_3__["SET_PAGING_OPTION"], function (prevState, action) {
  return _babel_runtime_corejs3_core_js_stable_object_assign__WEBPACK_IMPORTED_MODULE_1___default()({}, prevState, action.data);
}));

/***/ }),

/***/ "./node_modules/@9softstudio/react-fixed-table-header/dist/index.js":
/*!**************************************************************************!*\
  !*** ./node_modules/@9softstudio/react-fixed-table-header/dist/index.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

!function(e,t){ true?module.exports=t(__webpack_require__(/*! react */ "./node_modules/react/index.js")):undefined}(window,function(e){return function(e){var t={};function n(r){if(t[r])return t[r].exports;var o=t[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}return n.m=e,n.c=t,n.d=function(e,t,r){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:r})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)n.d(r,o,function(t){return e[t]}.bind(null,o));return r},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="/dist/",n(n.s=4)}([function(t,n){t.exports=e},function(e,t,n){e.exports=n(7)()},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});t.SCROLLBAR_WIDTH=19,t.GUTTER=15,t.DEFAULT_MILLISECOND_FOR_WAITING=500,t.DEFAULT_COLUMN_WIDTH=100},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=u(o),i=u(n(1));function u(e){return e&&e.__esModule?e:{default:e}}var l=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));return n.onKeyUpHandle=function(e,t){if(13===(e.keyCode?e.keyCode:e.which)){var r=parseInt(n.state.inputValue);r>0&&r<=t&&n.goToPage(r,n.props.pageOption.PageSize)}},n.onInputChangeHandler=function(e){var t=e.target.value;/^\d*$/.test(t)&&n._updateState(t)},n.state={inputValue:n.props.pageOption.PageIndex},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"componentDidUpdate",value:function(e,t){var n=this.props.pageOption.PageIndex;n!==e.pageOption.PageIndex&&this._updateState(n)}},{key:"goToPage",value:function(e,t){this.props.onPaging&&this.props.onPaging(e,t)}},{key:"onClickHandle",value:function(e,t){e||t()}},{key:"_updateState",value:function(e){this.setState({inputValue:e})}},{key:"_renderPageSizeSelection",value:function(e,t,n){var r=this;return a.default.createElement("select",{className:"pagesize-select",disabled:e,value:n,onChange:function(e){return r.goToPage(1,Number(e.target.value))}},this._renderPageSizeOption(this.props.pageOption.PageList))}},{key:"_renderPageSizeOption",value:function(e){return e.map(function(e,t){return a.default.createElement("option",{key:"pagesize"+t,value:e},e)})}},{key:"_renderPageIcon",value:function(e,t,n){var r=this;return a.default.createElement("a",{href:"javascript:;",className:"icon "+e+" "+(t?"disable":""),onClick:function(){return r.onClickHandle(t,n)}})}},{key:"_renderPageInput",value:function(e,t){var n=this;return a.default.createElement("div",{className:"page-input"},a.default.createElement("span",null,"Page "),a.default.createElement("input",{type:"text",className:"page-form",value:e,maxLength:"4",size:"2",onChange:this.onInputChangeHandler,onKeyUp:function(e){return n.onKeyUpHandle(e,t)}}),a.default.createElement("span",null," of "+t," "))}},{key:"_renderPageLabel",value:function(e,t){return a.default.createElement("div",{className:"page-input"},a.default.createElement("span",null,"Page ",e+" of "+t+" "))}},{key:"_renderPageInfo",value:function(e,t,n){var r=(e-1)*t+1,o=e*t;return a.default.createElement("span",{className:"page-info"},"View "+r+"-"+(o>n?n:o)+" of "+n)}},{key:"render",value:function(){var e=this,t=this.props,n=t.isShowPagingInfo,r=t.isAllowInputPageIndex,o=t.pageOption,i=o.PageIndex,u=o.PageSize,l=o.TotalItem,c=o.PageList,s=this.state.inputValue,f=isNaN(parseInt(s)),p=l>c[0],d=Math.ceil(l/u);return a.default.createElement("div",{className:"page-container center-align",style:{display:p?"block":"none"}},this._renderPageIcon("page-first",1===i,function(){return e.goToPage(1,u)}),this._renderPageIcon("page-prev",f||i<=1||i>d,function(){return e.goToPage(i-1,u)}),r?this._renderPageInput(s,d):this._renderPageLabel(i,d),this._renderPageIcon("page-next",f||i>=d||i<1,function(){return e.goToPage(i+1,u)}),this._renderPageIcon("page-last",i===d,function(){return e.goToPage(d,u)}),this._renderPageSizeSelection(f,i,u),n&&this._renderPageInfo(i,u,l))}}]),t}();l.propTypes={pageOption:i.default.shape({PageIndex:i.default.number,PageSize:i.default.number,PageList:i.default.arrayOf(i.default.number),TotalItem:i.default.number}),onPaging:i.default.func,isShowPagingInfo:i.default.bool,isAllowInputPageIndex:i.default.bool},l.defaultProps={pageOption:{PageIndex:1,PageSize:50,PageList:[50,100,200],TotalItem:0},isShowPagingInfo:!0,isAllowInputPageIndex:!0},t.default=l},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0}),t.Pager=t.Cell=t.Row=t.Table=void 0;var r=u(n(5)),o=u(n(10)),a=u(n(11)),i=u(n(3));function u(e){return e&&e.__esModule?e:{default:e}}t.Table=r.default,t.Row=o.default,t.Cell=a.default,t.Pager=i.default},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=p(a),u=p(n(6)),l=p(n(1)),c=p(n(9)),s=n(2),f=p(n(3));function p(e){return e&&e.__esModule?e:{default:e}}var d=document.body.clientWidth;var h=function(e){function t(e){!function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t);var n=function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e));y.call(n),n.bodyWrapper=null,n.footerWrapper=null;n.Header=(0,c.default)({className:"header-content",isHeader:!0});var r={className:"body-content",getRef:function(e){return n.bodyWrapper=e}};n.Body=(0,c.default)(r);var o={className:"footer-content",getRef:function(e){return n.footerWrapper=e}};n.Footer=(0,c.default)(o);var a=n.props,i=a.maxWidth,u=a.bodyHeight,l=a.adjustedHeight,f=i||d;return n.diffWidth=d-f,n.adjustedHeight=l,n.debounceResizing=function(e,t){var n=void 0;return function(){var r=this,o=arguments;clearTimeout(n),n=setTimeout(function(){e.apply(r,o)},t||s.DEFAULT_MILLISECOND_FOR_WAITING)}}(n._handleResize),n.state={maxWidth:f,contentHeight:u,columnsWidth:n._getColumnsWidth(e.header)},n}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,a.Component),o(t,[{key:"_getColumnsWidth",value:function(e){var t=this,n=Array.isArray(e);if(n&&0===e.length)return null;var r=[],o={};return function n(a,i){for(var u=0;u<a.length;u++){var l=a[u];if(l){var c=l.props,f=Number(c.colSpan);if(f&&f>1){var p=i+1;void 0===o[p]&&(o[p]=0);var d=o[p],h=d+f-1,y=e[p].props.children,b=t._getNextColspanCells(y,d,h);o[p]+=b.length,n(b,p)}else{var g=c.colWidth?c.colWidth:s.DEFAULT_COLUMN_WIDTH;r.push(g)}}}}(n?e[0].props.children:e.props.children,0),r}},{key:"_getNextColspanCells",value:function(e,t,n){for(var r=[],o=t;o<=n;o++){var a=e[o],i=o<e.length;a||!i?(a.props.colSpan&&(n-=1),r.push(a)):n+=1}return r}},{key:"componentDidMount",value:function(){this._handleResize(),window.addEventListener("resize",this.debounceResizing)}},{key:"componentWillUnmount",value:function(){window.removeEventListener("resize",this.debounceResizing)}},{key:"componentDidUpdate",value:function(e,t){this.props.header!=e.header&&this.setState({columnsWidth:this._getColumnsWidth(this.props.header)}),this.props.shouldResetScrollPosition&&e.body!=this.props.body&&function(e,t){var n=-e.scrollTop/(t/15);requestAnimationFrame(function(){var t=null;t=setInterval(function(){0!=e.scrollTop?e.scrollTop+=n:clearInterval(t)},15)})}(this.bodyWrapper,200)}},{key:"_calculateBodyHeight",value:function(){var e=window.innerHeight,t=this.footerWrapper?this.footerWrapper.offsetHeight:0;return e-(this.bodyWrapper.offsetTop+t+70)-this.adjustedHeight}},{key:"_getUpdatedColumnLayout",value:function(){var e=this,t=this.props,n=t.width,r=t.autoWidth,o=t.containerPadding,a=r?this.state.maxWidth:n;return a=a-o-s.SCROLLBAR_WIDTH,this.columnWidthSum&&this.state.columnsWidth.map(function(t){return a/e.columnWidthSum*t})}},{key:"render",value:function(){var e=this.props,t=e.width,n=e.autoWidth,o=e.minWidth,a=e.header,l=e.body,c=e.footer,s=e.isPaging,p=e.pageOption,d=e.onPaging,h=e.containerPadding,y=this.state.maxWidth-h,b=t-h,g=this.columnWidthSum&&this.columnWidthSum!==b?this._getUpdatedColumnLayout():this.state.columnsWidth;if(!g)return null;var m={width:b,autoWidth:n,minWidth:o,maxWidth:y},v=i.default.createElement(u.default,{columnLayout:g}),_=this.Header,O=this.Body,P=this.Footer;return i.default.createElement("div",null,i.default.createElement("div",{className:"table-container",style:{maxWidth:y}},i.default.createElement(_,m,v,a),l&&i.default.createElement(O,r({},m,{maxHeight:this.state.contentHeight}),v,l),c&&i.default.createElement(P,m,v,c)),s&&p&&i.default.createElement(f.default,{pageOption:p,onPaging:d}))}},{key:"columnWidthSum",get:function(){var e=this.state.columnsWidth;return e&&e.length&&!e.find(function(e){return"number"!=typeof e})?e.reduce(function(e,t){return e+t},0):null}}]),t}();h.propTypes={width:l.default.oneOfType([l.default.number,l.default.string]),maxWidth:l.default.number,minWidth:l.default.number,autoWidth:l.default.bool,bodyHeight:l.default.number,autoHeight:l.default.bool,header:l.default.oneOfType([l.default.object,l.default.arrayOf(l.default.object)]).isRequired,body:l.default.arrayOf(l.default.object),footer:l.default.arrayOf(l.default.object),isPaging:l.default.bool,onPaging:l.default.func,pageOption:l.default.object,adjustedHeight:l.default.number,containerPadding:l.default.number,shouldResetScrollPosition:l.default.bool},h.defaultProps={width:d,maxWidth:d,autoWidth:!0,isPaging:!1,adjustedHeight:0,bodyHeight:0,autoHeight:!0,containerPadding:30,shouldResetScrollPosition:!0};var y=function(){var e=this;this._handleResize=function(t){if(t&&t.preventDefault(),e.bodyWrapper&&e.props.autoHeight){var n=e._calculateBodyHeight();e.state.contentHeight!==n&&e.setState({contentHeight:n})}var r=document.body.clientWidth-e.diffWidth;e.state.maxWidth!==r&&e.setState({maxWidth:r})}};t.default=h},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=u(o),i=u(n(1));function u(e){return e&&e.__esModule?e:{default:e}}var l=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"_getRowLayout",value:function(){return this.props.columnLayout.map(function(e,t){var n=isNaN(e)?e:e+"px";return a.default.createElement("td",{key:"cellLayout"+t,style:{width:n}})})}},{key:"render",value:function(){return a.default.createElement("tr",{className:"first-row"},this._getRowLayout())}}]),t}();l.propTypes={columnLayout:i.default.arrayOf(i.default.oneOfType([i.default.number,i.default.string])).isRequired},t.default=l},function(e,t,n){"use strict";var r=n(8);function o(){}e.exports=function(){function e(e,t,n,o,a,i){if(i!==r){var u=new Error("Calling PropTypes validators directly is not supported by the `prop-types` package. Use PropTypes.checkPropTypes() to call them. Read more at http://fb.me/use-check-prop-types");throw u.name="Invariant Violation",u}}function t(){return e}e.isRequired=e;var n={array:e,bool:e,func:e,number:e,object:e,string:e,symbol:e,any:e,arrayOf:t,element:e,instanceOf:t,node:e,objectOf:t,oneOf:t,oneOfType:t,shape:t,exact:t};return n.checkPropTypes=o,n.PropTypes=n,n}},function(e,t,n){"use strict";e.exports="SECRET_DO_NOT_PASS_THIS_OR_YOU_WILL_BE_FIRED"},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=c(a),u=c(n(1)),l=n(2);function c(e){return e&&e.__esModule?e:{default:e}}function s(e,t){var n={};for(var r in e)t.indexOf(r)>=0||Object.prototype.hasOwnProperty.call(e,r)&&(n[r]=e[r]);return n}t.default=function(e,t){var n,c;return c=n=function(n){function u(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,u),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(u.__proto__||Object.getPrototypeOf(u)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(u,a.Component),o(u,[{key:"render",value:function(){var n=this.props,o=n.tableClass,a=(n.width,n.autoWidth,n.maxWidth,n.minWidth,n.maxHeight,s(n,["tableClass","width","autoWidth","maxWidth","minWidth","maxHeight"])),u=e.isHeader,l=e.getRef,c=s(e,["isHeader","getRef"]);return i.default.createElement("div",r({},a,{style:this.containerStyle,ref:l},c),i.default.createElement("div",{className:"table-wrapper"},i.default.createElement("table",r({style:this.tableStyle,className:o},t),u?i.default.createElement("thead",null,this.props.children):i.default.createElement("tbody",null,this.props.children))))}},{key:"containerWidth",get:function(){var e=this.props,t=e.autoWidth,n=e.maxWidth,r=e.width;return t?n:r}},{key:"containerStyle",get:function(){var e=this.props,t=e.minWidth,n=e.maxHeight;return r({},this.props.style,{width:this.containerWidth,minWidth:t,maxHeight:n})}},{key:"tableStyle",get:function(){var e=this.props.minWidth;return{width:(e?Math.max(this.containerWidth,e):this.containerWidth)-l.SCROLLBAR_WIDTH}}}]),u}(),n.propTypes={tableClass:u.default.string,width:u.default.oneOfType([u.default.number,u.default.string]).isRequired,maxWidth:u.default.number.isRequired,minWidth:u.default.number,autoWidth:u.default.bool,maxHeight:u.default.number},n.defaultProps={tableClass:"table",autoWidth:!0},c}},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),o=n(0),a=function(e){return e&&e.__esModule?e:{default:e}}(o);var i=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,o.Component),r(t,[{key:"render",value:function(){return a.default.createElement("tr",this.props,this.props.children)}}]),t}();t.default=i},function(e,t,n){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var r=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var n=arguments[t];for(var r in n)Object.prototype.hasOwnProperty.call(n,r)&&(e[r]=n[r])}return e},o=function(){function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(t,n,r){return n&&e(t.prototype,n),r&&e(t,r),t}}(),a=n(0),i=l(a),u=l(n(1));function l(e){return e&&e.__esModule?e:{default:e}}var c=function(e){function t(e){return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,t),function(e,t){if(!e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return!t||"object"!=typeof t&&"function"!=typeof t?e:t}(this,(t.__proto__||Object.getPrototypeOf(t)).call(this,e))}return function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function, not "+typeof t);e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}}),t&&(Object.setPrototypeOf?Object.setPrototypeOf(e,t):e.__proto__=t)}(t,a.Component),o(t,[{key:"render",value:function(){var e=this.props,t=e.header,n=(e.colWidth,e.sortable),o=e.sorting,a=e.asc,u=e.onClick,l=function(e,t){var n={};for(var r in e)t.indexOf(r)>=0||Object.prototype.hasOwnProperty.call(e,r)&&(n[r]=e[r]);return n}(e,["header","colWidth","sortable","sorting","asc","onClick"]);return t?n?i.default.createElement("th",r({},l,{className:"sortable "+(o?"sorting":""),onClick:u}),this.props.children,i.default.createElement("span",{className:o?a?"asc":"desc":""})):i.default.createElement("th",l,this.props.children):i.default.createElement("td",l,this.props.children)}}]),t}();c.propTypes={header:u.default.bool,sortable:u.default.bool,sorting:u.default.bool,asc:u.default.bool,onClick:u.default.func},c.defaultProps={header:!1,sortable:!1,sorting:!1,asc:!0,onClick:function(){}},t.default=c}])});

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/array/from.js":
/*!**************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/array/from.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/array/from */ "./node_modules/core-js-pure/stable/array/from.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js":
/*!*******************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/concat.js ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/concat */ "./node_modules/core-js-pure/stable/instance/concat.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/includes.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/includes.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/includes */ "./node_modules/core-js-pure/stable/instance/includes.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/index-of.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/index-of.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/index-of */ "./node_modules/core-js-pure/stable/instance/index-of.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/last-index-of.js":
/*!**************************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/last-index-of.js ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/last-index-of */ "./node_modules/core-js-pure/stable/instance/last-index-of.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js":
/*!*******************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/reduce.js ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/reduce */ "./node_modules/core-js-pure/stable/instance/reduce.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/slice.js":
/*!******************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/slice.js ***!
  \******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/slice */ "./node_modules/core-js-pure/stable/instance/slice.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/instance/trim.js":
/*!*****************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/instance/trim.js ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/instance/trim */ "./node_modules/core-js-pure/stable/instance/trim.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js":
/*!*****************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/assign.js ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/assign */ "./node_modules/core-js-pure/stable/object/assign.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/define-properties.js":
/*!****************************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/define-properties.js ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/define-properties */ "./node_modules/core-js-pure/stable/object/define-properties.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/define-property.js":
/*!**************************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/define-property.js ***!
  \**************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/define-property */ "./node_modules/core-js-pure/stable/object/define-property.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptor.js":
/*!**************************************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptor.js ***!
  \**************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/get-own-property-descriptor */ "./node_modules/core-js-pure/stable/object/get-own-property-descriptor.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptors.js":
/*!***************************************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-descriptors.js ***!
  \***************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/get-own-property-descriptors */ "./node_modules/core-js-pure/stable/object/get-own-property-descriptors.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-symbols.js":
/*!***********************************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/get-own-property-symbols.js ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/get-own-property-symbols */ "./node_modules/core-js-pure/stable/object/get-own-property-symbols.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js":
/*!***************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/object/keys.js ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/object/keys */ "./node_modules/core-js-pure/stable/object/keys.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js-stable/promise.js":
/*!***********************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js-stable/promise.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/stable/promise */ "./node_modules/core-js-pure/stable/promise/index.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js/array/from.js":
/*!*******************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js/array/from.js ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/features/array/from */ "./node_modules/core-js-pure/features/array/from.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js/array/is-array.js":
/*!***********************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js/array/is-array.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/features/array/is-array */ "./node_modules/core-js-pure/features/array/is-array.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js/instance/slice.js":
/*!***********************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js/instance/slice.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/features/instance/slice */ "./node_modules/core-js-pure/features/instance/slice.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js/is-iterable.js":
/*!********************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js/is-iterable.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/features/is-iterable */ "./node_modules/core-js-pure/features/is-iterable.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/core-js/promise.js":
/*!****************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/core-js/promise.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! core-js-pure/features/promise */ "./node_modules/core-js-pure/features/promise/index.js");

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/arrayLikeToArray.js":
/*!*************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/arrayLikeToArray.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function _arrayLikeToArray(arr, len) {
  if (len == null || len > arr.length) len = arr.length;

  for (var i = 0, arr2 = new Array(len); i < len; i++) {
    arr2[i] = arr[i];
  }

  return arr2;
}

module.exports = _arrayLikeToArray;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/arrayWithoutHoles.js":
/*!**************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/arrayWithoutHoles.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var _Array$isArray = __webpack_require__(/*! @babel/runtime-corejs3/core-js/array/is-array */ "./node_modules/@babel/runtime-corejs3/core-js/array/is-array.js");

var arrayLikeToArray = __webpack_require__(/*! ./arrayLikeToArray */ "./node_modules/@babel/runtime-corejs3/helpers/arrayLikeToArray.js");

function _arrayWithoutHoles(arr) {
  if (_Array$isArray(arr)) return arrayLikeToArray(arr);
}

module.exports = _arrayWithoutHoles;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js":
/*!*************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/asyncToGenerator.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var _Promise = __webpack_require__(/*! @babel/runtime-corejs3/core-js/promise */ "./node_modules/@babel/runtime-corejs3/core-js/promise.js");

function asyncGeneratorStep(gen, resolve, reject, _next, _throw, key, arg) {
  try {
    var info = gen[key](arg);
    var value = info.value;
  } catch (error) {
    reject(error);
    return;
  }

  if (info.done) {
    resolve(value);
  } else {
    _Promise.resolve(value).then(_next, _throw);
  }
}

function _asyncToGenerator(fn) {
  return function () {
    var self = this,
        args = arguments;
    return new _Promise(function (resolve, reject) {
      var gen = fn.apply(self, args);

      function _next(value) {
        asyncGeneratorStep(gen, resolve, reject, _next, _throw, "next", value);
      }

      function _throw(err) {
        asyncGeneratorStep(gen, resolve, reject, _next, _throw, "throw", err);
      }

      _next(undefined);
    });
  };
}

module.exports = _asyncToGenerator;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/iterableToArray.js":
/*!************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/iterableToArray.js ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var _Array$from = __webpack_require__(/*! @babel/runtime-corejs3/core-js/array/from */ "./node_modules/@babel/runtime-corejs3/core-js/array/from.js");

var _isIterable = __webpack_require__(/*! @babel/runtime-corejs3/core-js/is-iterable */ "./node_modules/@babel/runtime-corejs3/core-js/is-iterable.js");

var _Symbol = __webpack_require__(/*! @babel/runtime-corejs3/core-js/symbol */ "./node_modules/@babel/runtime-corejs3/core-js/symbol.js");

function _iterableToArray(iter) {
  if (typeof _Symbol !== "undefined" && _isIterable(Object(iter))) return _Array$from(iter);
}

module.exports = _iterableToArray;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/nonIterableSpread.js":
/*!**************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/nonIterableSpread.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function _nonIterableSpread() {
  throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
}

module.exports = _nonIterableSpread;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/toConsumableArray.js":
/*!**************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/toConsumableArray.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var arrayWithoutHoles = __webpack_require__(/*! ./arrayWithoutHoles */ "./node_modules/@babel/runtime-corejs3/helpers/arrayWithoutHoles.js");

var iterableToArray = __webpack_require__(/*! ./iterableToArray */ "./node_modules/@babel/runtime-corejs3/helpers/iterableToArray.js");

var unsupportedIterableToArray = __webpack_require__(/*! ./unsupportedIterableToArray */ "./node_modules/@babel/runtime-corejs3/helpers/unsupportedIterableToArray.js");

var nonIterableSpread = __webpack_require__(/*! ./nonIterableSpread */ "./node_modules/@babel/runtime-corejs3/helpers/nonIterableSpread.js");

function _toConsumableArray(arr) {
  return arrayWithoutHoles(arr) || iterableToArray(arr) || unsupportedIterableToArray(arr) || nonIterableSpread();
}

module.exports = _toConsumableArray;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/helpers/unsupportedIterableToArray.js":
/*!***********************************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/helpers/unsupportedIterableToArray.js ***!
  \***********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var _Array$from = __webpack_require__(/*! @babel/runtime-corejs3/core-js/array/from */ "./node_modules/@babel/runtime-corejs3/core-js/array/from.js");

var _sliceInstanceProperty = __webpack_require__(/*! @babel/runtime-corejs3/core-js/instance/slice */ "./node_modules/@babel/runtime-corejs3/core-js/instance/slice.js");

var arrayLikeToArray = __webpack_require__(/*! ./arrayLikeToArray */ "./node_modules/@babel/runtime-corejs3/helpers/arrayLikeToArray.js");

function _unsupportedIterableToArray(o, minLen) {
  var _context;

  if (!o) return;
  if (typeof o === "string") return arrayLikeToArray(o, minLen);

  var n = _sliceInstanceProperty(_context = Object.prototype.toString.call(o)).call(_context, 8, -1);

  if (n === "Object" && o.constructor) n = o.constructor.name;
  if (n === "Map" || n === "Set") return _Array$from(o);
  if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return arrayLikeToArray(o, minLen);
}

module.exports = _unsupportedIterableToArray;

/***/ }),

/***/ "./node_modules/@babel/runtime-corejs3/regenerator/index.js":
/*!******************************************************************!*\
  !*** ./node_modules/@babel/runtime-corejs3/regenerator/index.js ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! regenerator-runtime */ "./node_modules/regenerator-runtime/runtime.js");


/***/ }),

/***/ "./node_modules/axios/index.js":
/*!*************************************!*\
  !*** ./node_modules/axios/index.js ***!
  \*************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! ./lib/axios */ "./node_modules/axios/lib/axios.js");

/***/ }),

/***/ "./node_modules/axios/lib/adapters/xhr.js":
/*!************************************************!*\
  !*** ./node_modules/axios/lib/adapters/xhr.js ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");
var settle = __webpack_require__(/*! ./../core/settle */ "./node_modules/axios/lib/core/settle.js");
var buildURL = __webpack_require__(/*! ./../helpers/buildURL */ "./node_modules/axios/lib/helpers/buildURL.js");
var parseHeaders = __webpack_require__(/*! ./../helpers/parseHeaders */ "./node_modules/axios/lib/helpers/parseHeaders.js");
var isURLSameOrigin = __webpack_require__(/*! ./../helpers/isURLSameOrigin */ "./node_modules/axios/lib/helpers/isURLSameOrigin.js");
var createError = __webpack_require__(/*! ../core/createError */ "./node_modules/axios/lib/core/createError.js");

module.exports = function xhrAdapter(config) {
  return new Promise(function dispatchXhrRequest(resolve, reject) {
    var requestData = config.data;
    var requestHeaders = config.headers;

    if (utils.isFormData(requestData)) {
      delete requestHeaders['Content-Type']; // Let the browser set it
    }

    var request = new XMLHttpRequest();

    // HTTP basic authentication
    if (config.auth) {
      var username = config.auth.username || '';
      var password = config.auth.password || '';
      requestHeaders.Authorization = 'Basic ' + btoa(username + ':' + password);
    }

    request.open(config.method.toUpperCase(), buildURL(config.url, config.params, config.paramsSerializer), true);

    // Set the request timeout in MS
    request.timeout = config.timeout;

    // Listen for ready state
    request.onreadystatechange = function handleLoad() {
      if (!request || request.readyState !== 4) {
        return;
      }

      // The request errored out and we didn't get a response, this will be
      // handled by onerror instead
      // With one exception: request that using file: protocol, most browsers
      // will return status as 0 even though it's a successful request
      if (request.status === 0 && !(request.responseURL && request.responseURL.indexOf('file:') === 0)) {
        return;
      }

      // Prepare the response
      var responseHeaders = 'getAllResponseHeaders' in request ? parseHeaders(request.getAllResponseHeaders()) : null;
      var responseData = !config.responseType || config.responseType === 'text' ? request.responseText : request.response;
      var response = {
        data: responseData,
        status: request.status,
        statusText: request.statusText,
        headers: responseHeaders,
        config: config,
        request: request
      };

      settle(resolve, reject, response);

      // Clean up request
      request = null;
    };

    // Handle low level network errors
    request.onerror = function handleError() {
      // Real errors are hidden from us by the browser
      // onerror should only fire if it's a network error
      reject(createError('Network Error', config, null, request));

      // Clean up request
      request = null;
    };

    // Handle timeout
    request.ontimeout = function handleTimeout() {
      reject(createError('timeout of ' + config.timeout + 'ms exceeded', config, 'ECONNABORTED',
        request));

      // Clean up request
      request = null;
    };

    // Add xsrf header
    // This is only done if running in a standard browser environment.
    // Specifically not if we're in a web worker, or react-native.
    if (utils.isStandardBrowserEnv()) {
      var cookies = __webpack_require__(/*! ./../helpers/cookies */ "./node_modules/axios/lib/helpers/cookies.js");

      // Add xsrf header
      var xsrfValue = (config.withCredentials || isURLSameOrigin(config.url)) && config.xsrfCookieName ?
          cookies.read(config.xsrfCookieName) :
          undefined;

      if (xsrfValue) {
        requestHeaders[config.xsrfHeaderName] = xsrfValue;
      }
    }

    // Add headers to the request
    if ('setRequestHeader' in request) {
      utils.forEach(requestHeaders, function setRequestHeader(val, key) {
        if (typeof requestData === 'undefined' && key.toLowerCase() === 'content-type') {
          // Remove Content-Type if data is undefined
          delete requestHeaders[key];
        } else {
          // Otherwise add header to the request
          request.setRequestHeader(key, val);
        }
      });
    }

    // Add withCredentials to request if needed
    if (config.withCredentials) {
      request.withCredentials = true;
    }

    // Add responseType to request if needed
    if (config.responseType) {
      try {
        request.responseType = config.responseType;
      } catch (e) {
        // Expected DOMException thrown by browsers not compatible XMLHttpRequest Level 2.
        // But, this can be suppressed for 'json' type as it can be parsed by default 'transformResponse' function.
        if (config.responseType !== 'json') {
          throw e;
        }
      }
    }

    // Handle progress if needed
    if (typeof config.onDownloadProgress === 'function') {
      request.addEventListener('progress', config.onDownloadProgress);
    }

    // Not all browsers support upload events
    if (typeof config.onUploadProgress === 'function' && request.upload) {
      request.upload.addEventListener('progress', config.onUploadProgress);
    }

    if (config.cancelToken) {
      // Handle cancellation
      config.cancelToken.promise.then(function onCanceled(cancel) {
        if (!request) {
          return;
        }

        request.abort();
        reject(cancel);
        // Clean up request
        request = null;
      });
    }

    if (requestData === undefined) {
      requestData = null;
    }

    // Send the request
    request.send(requestData);
  });
};


/***/ }),

/***/ "./node_modules/axios/lib/axios.js":
/*!*****************************************!*\
  !*** ./node_modules/axios/lib/axios.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./utils */ "./node_modules/axios/lib/utils.js");
var bind = __webpack_require__(/*! ./helpers/bind */ "./node_modules/axios/lib/helpers/bind.js");
var Axios = __webpack_require__(/*! ./core/Axios */ "./node_modules/axios/lib/core/Axios.js");
var defaults = __webpack_require__(/*! ./defaults */ "./node_modules/axios/lib/defaults.js");

/**
 * Create an instance of Axios
 *
 * @param {Object} defaultConfig The default config for the instance
 * @return {Axios} A new instance of Axios
 */
function createInstance(defaultConfig) {
  var context = new Axios(defaultConfig);
  var instance = bind(Axios.prototype.request, context);

  // Copy axios.prototype to instance
  utils.extend(instance, Axios.prototype, context);

  // Copy context to instance
  utils.extend(instance, context);

  return instance;
}

// Create the default instance to be exported
var axios = createInstance(defaults);

// Expose Axios class to allow class inheritance
axios.Axios = Axios;

// Factory for creating new instances
axios.create = function create(instanceConfig) {
  return createInstance(utils.merge(defaults, instanceConfig));
};

// Expose Cancel & CancelToken
axios.Cancel = __webpack_require__(/*! ./cancel/Cancel */ "./node_modules/axios/lib/cancel/Cancel.js");
axios.CancelToken = __webpack_require__(/*! ./cancel/CancelToken */ "./node_modules/axios/lib/cancel/CancelToken.js");
axios.isCancel = __webpack_require__(/*! ./cancel/isCancel */ "./node_modules/axios/lib/cancel/isCancel.js");

// Expose all/spread
axios.all = function all(promises) {
  return Promise.all(promises);
};
axios.spread = __webpack_require__(/*! ./helpers/spread */ "./node_modules/axios/lib/helpers/spread.js");

module.exports = axios;

// Allow use of default import syntax in TypeScript
module.exports.default = axios;


/***/ }),

/***/ "./node_modules/axios/lib/cancel/Cancel.js":
/*!*************************************************!*\
  !*** ./node_modules/axios/lib/cancel/Cancel.js ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * A `Cancel` is an object that is thrown when an operation is canceled.
 *
 * @class
 * @param {string=} message The message.
 */
function Cancel(message) {
  this.message = message;
}

Cancel.prototype.toString = function toString() {
  return 'Cancel' + (this.message ? ': ' + this.message : '');
};

Cancel.prototype.__CANCEL__ = true;

module.exports = Cancel;


/***/ }),

/***/ "./node_modules/axios/lib/cancel/CancelToken.js":
/*!******************************************************!*\
  !*** ./node_modules/axios/lib/cancel/CancelToken.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var Cancel = __webpack_require__(/*! ./Cancel */ "./node_modules/axios/lib/cancel/Cancel.js");

/**
 * A `CancelToken` is an object that can be used to request cancellation of an operation.
 *
 * @class
 * @param {Function} executor The executor function.
 */
function CancelToken(executor) {
  if (typeof executor !== 'function') {
    throw new TypeError('executor must be a function.');
  }

  var resolvePromise;
  this.promise = new Promise(function promiseExecutor(resolve) {
    resolvePromise = resolve;
  });

  var token = this;
  executor(function cancel(message) {
    if (token.reason) {
      // Cancellation has already been requested
      return;
    }

    token.reason = new Cancel(message);
    resolvePromise(token.reason);
  });
}

/**
 * Throws a `Cancel` if cancellation has been requested.
 */
CancelToken.prototype.throwIfRequested = function throwIfRequested() {
  if (this.reason) {
    throw this.reason;
  }
};

/**
 * Returns an object that contains a new `CancelToken` and a function that, when called,
 * cancels the `CancelToken`.
 */
CancelToken.source = function source() {
  var cancel;
  var token = new CancelToken(function executor(c) {
    cancel = c;
  });
  return {
    token: token,
    cancel: cancel
  };
};

module.exports = CancelToken;


/***/ }),

/***/ "./node_modules/axios/lib/cancel/isCancel.js":
/*!***************************************************!*\
  !*** ./node_modules/axios/lib/cancel/isCancel.js ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


module.exports = function isCancel(value) {
  return !!(value && value.__CANCEL__);
};


/***/ }),

/***/ "./node_modules/axios/lib/core/Axios.js":
/*!**********************************************!*\
  !*** ./node_modules/axios/lib/core/Axios.js ***!
  \**********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var defaults = __webpack_require__(/*! ./../defaults */ "./node_modules/axios/lib/defaults.js");
var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");
var InterceptorManager = __webpack_require__(/*! ./InterceptorManager */ "./node_modules/axios/lib/core/InterceptorManager.js");
var dispatchRequest = __webpack_require__(/*! ./dispatchRequest */ "./node_modules/axios/lib/core/dispatchRequest.js");

/**
 * Create a new instance of Axios
 *
 * @param {Object} instanceConfig The default config for the instance
 */
function Axios(instanceConfig) {
  this.defaults = instanceConfig;
  this.interceptors = {
    request: new InterceptorManager(),
    response: new InterceptorManager()
  };
}

/**
 * Dispatch a request
 *
 * @param {Object} config The config specific for this request (merged with this.defaults)
 */
Axios.prototype.request = function request(config) {
  /*eslint no-param-reassign:0*/
  // Allow for axios('example/url'[, config]) a la fetch API
  if (typeof config === 'string') {
    config = utils.merge({
      url: arguments[0]
    }, arguments[1]);
  }

  config = utils.merge(defaults, {method: 'get'}, this.defaults, config);
  config.method = config.method.toLowerCase();

  // Hook up interceptors middleware
  var chain = [dispatchRequest, undefined];
  var promise = Promise.resolve(config);

  this.interceptors.request.forEach(function unshiftRequestInterceptors(interceptor) {
    chain.unshift(interceptor.fulfilled, interceptor.rejected);
  });

  this.interceptors.response.forEach(function pushResponseInterceptors(interceptor) {
    chain.push(interceptor.fulfilled, interceptor.rejected);
  });

  while (chain.length) {
    promise = promise.then(chain.shift(), chain.shift());
  }

  return promise;
};

// Provide aliases for supported request methods
utils.forEach(['delete', 'get', 'head', 'options'], function forEachMethodNoData(method) {
  /*eslint func-names:0*/
  Axios.prototype[method] = function(url, config) {
    return this.request(utils.merge(config || {}, {
      method: method,
      url: url
    }));
  };
});

utils.forEach(['post', 'put', 'patch'], function forEachMethodWithData(method) {
  /*eslint func-names:0*/
  Axios.prototype[method] = function(url, data, config) {
    return this.request(utils.merge(config || {}, {
      method: method,
      url: url,
      data: data
    }));
  };
});

module.exports = Axios;


/***/ }),

/***/ "./node_modules/axios/lib/core/InterceptorManager.js":
/*!***********************************************************!*\
  !*** ./node_modules/axios/lib/core/InterceptorManager.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

function InterceptorManager() {
  this.handlers = [];
}

/**
 * Add a new interceptor to the stack
 *
 * @param {Function} fulfilled The function to handle `then` for a `Promise`
 * @param {Function} rejected The function to handle `reject` for a `Promise`
 *
 * @return {Number} An ID used to remove interceptor later
 */
InterceptorManager.prototype.use = function use(fulfilled, rejected) {
  this.handlers.push({
    fulfilled: fulfilled,
    rejected: rejected
  });
  return this.handlers.length - 1;
};

/**
 * Remove an interceptor from the stack
 *
 * @param {Number} id The ID that was returned by `use`
 */
InterceptorManager.prototype.eject = function eject(id) {
  if (this.handlers[id]) {
    this.handlers[id] = null;
  }
};

/**
 * Iterate over all the registered interceptors
 *
 * This method is particularly useful for skipping over any
 * interceptors that may have become `null` calling `eject`.
 *
 * @param {Function} fn The function to call for each interceptor
 */
InterceptorManager.prototype.forEach = function forEach(fn) {
  utils.forEach(this.handlers, function forEachHandler(h) {
    if (h !== null) {
      fn(h);
    }
  });
};

module.exports = InterceptorManager;


/***/ }),

/***/ "./node_modules/axios/lib/core/createError.js":
/*!****************************************************!*\
  !*** ./node_modules/axios/lib/core/createError.js ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var enhanceError = __webpack_require__(/*! ./enhanceError */ "./node_modules/axios/lib/core/enhanceError.js");

/**
 * Create an Error with the specified message, config, error code, request and response.
 *
 * @param {string} message The error message.
 * @param {Object} config The config.
 * @param {string} [code] The error code (for example, 'ECONNABORTED').
 * @param {Object} [request] The request.
 * @param {Object} [response] The response.
 * @returns {Error} The created error.
 */
module.exports = function createError(message, config, code, request, response) {
  var error = new Error(message);
  return enhanceError(error, config, code, request, response);
};


/***/ }),

/***/ "./node_modules/axios/lib/core/dispatchRequest.js":
/*!********************************************************!*\
  !*** ./node_modules/axios/lib/core/dispatchRequest.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");
var transformData = __webpack_require__(/*! ./transformData */ "./node_modules/axios/lib/core/transformData.js");
var isCancel = __webpack_require__(/*! ../cancel/isCancel */ "./node_modules/axios/lib/cancel/isCancel.js");
var defaults = __webpack_require__(/*! ../defaults */ "./node_modules/axios/lib/defaults.js");
var isAbsoluteURL = __webpack_require__(/*! ./../helpers/isAbsoluteURL */ "./node_modules/axios/lib/helpers/isAbsoluteURL.js");
var combineURLs = __webpack_require__(/*! ./../helpers/combineURLs */ "./node_modules/axios/lib/helpers/combineURLs.js");

/**
 * Throws a `Cancel` if cancellation has been requested.
 */
function throwIfCancellationRequested(config) {
  if (config.cancelToken) {
    config.cancelToken.throwIfRequested();
  }
}

/**
 * Dispatch a request to the server using the configured adapter.
 *
 * @param {object} config The config that is to be used for the request
 * @returns {Promise} The Promise to be fulfilled
 */
module.exports = function dispatchRequest(config) {
  throwIfCancellationRequested(config);

  // Support baseURL config
  if (config.baseURL && !isAbsoluteURL(config.url)) {
    config.url = combineURLs(config.baseURL, config.url);
  }

  // Ensure headers exist
  config.headers = config.headers || {};

  // Transform request data
  config.data = transformData(
    config.data,
    config.headers,
    config.transformRequest
  );

  // Flatten headers
  config.headers = utils.merge(
    config.headers.common || {},
    config.headers[config.method] || {},
    config.headers || {}
  );

  utils.forEach(
    ['delete', 'get', 'head', 'post', 'put', 'patch', 'common'],
    function cleanHeaderConfig(method) {
      delete config.headers[method];
    }
  );

  var adapter = config.adapter || defaults.adapter;

  return adapter(config).then(function onAdapterResolution(response) {
    throwIfCancellationRequested(config);

    // Transform response data
    response.data = transformData(
      response.data,
      response.headers,
      config.transformResponse
    );

    return response;
  }, function onAdapterRejection(reason) {
    if (!isCancel(reason)) {
      throwIfCancellationRequested(config);

      // Transform response data
      if (reason && reason.response) {
        reason.response.data = transformData(
          reason.response.data,
          reason.response.headers,
          config.transformResponse
        );
      }
    }

    return Promise.reject(reason);
  });
};


/***/ }),

/***/ "./node_modules/axios/lib/core/enhanceError.js":
/*!*****************************************************!*\
  !*** ./node_modules/axios/lib/core/enhanceError.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Update an Error with the specified config, error code, and response.
 *
 * @param {Error} error The error to update.
 * @param {Object} config The config.
 * @param {string} [code] The error code (for example, 'ECONNABORTED').
 * @param {Object} [request] The request.
 * @param {Object} [response] The response.
 * @returns {Error} The error.
 */
module.exports = function enhanceError(error, config, code, request, response) {
  error.config = config;
  if (code) {
    error.code = code;
  }
  error.request = request;
  error.response = response;
  return error;
};


/***/ }),

/***/ "./node_modules/axios/lib/core/settle.js":
/*!***********************************************!*\
  !*** ./node_modules/axios/lib/core/settle.js ***!
  \***********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var createError = __webpack_require__(/*! ./createError */ "./node_modules/axios/lib/core/createError.js");

/**
 * Resolve or reject a Promise based on response status.
 *
 * @param {Function} resolve A function that resolves the promise.
 * @param {Function} reject A function that rejects the promise.
 * @param {object} response The response.
 */
module.exports = function settle(resolve, reject, response) {
  var validateStatus = response.config.validateStatus;
  // Note: status is not exposed by XDomainRequest
  if (!response.status || !validateStatus || validateStatus(response.status)) {
    resolve(response);
  } else {
    reject(createError(
      'Request failed with status code ' + response.status,
      response.config,
      null,
      response.request,
      response
    ));
  }
};


/***/ }),

/***/ "./node_modules/axios/lib/core/transformData.js":
/*!******************************************************!*\
  !*** ./node_modules/axios/lib/core/transformData.js ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

/**
 * Transform the data for a request or a response
 *
 * @param {Object|String} data The data to be transformed
 * @param {Array} headers The headers for the request or response
 * @param {Array|Function} fns A single function or Array of functions
 * @returns {*} The resulting transformed data
 */
module.exports = function transformData(data, headers, fns) {
  /*eslint no-param-reassign:0*/
  utils.forEach(fns, function transform(fn) {
    data = fn(data, headers);
  });

  return data;
};


/***/ }),

/***/ "./node_modules/axios/lib/defaults.js":
/*!********************************************!*\
  !*** ./node_modules/axios/lib/defaults.js ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(process) {

var utils = __webpack_require__(/*! ./utils */ "./node_modules/axios/lib/utils.js");
var normalizeHeaderName = __webpack_require__(/*! ./helpers/normalizeHeaderName */ "./node_modules/axios/lib/helpers/normalizeHeaderName.js");

var DEFAULT_CONTENT_TYPE = {
  'Content-Type': 'application/x-www-form-urlencoded'
};

function setContentTypeIfUnset(headers, value) {
  if (!utils.isUndefined(headers) && utils.isUndefined(headers['Content-Type'])) {
    headers['Content-Type'] = value;
  }
}

function getDefaultAdapter() {
  var adapter;
  if (typeof XMLHttpRequest !== 'undefined') {
    // For browsers use XHR adapter
    adapter = __webpack_require__(/*! ./adapters/xhr */ "./node_modules/axios/lib/adapters/xhr.js");
  } else if (typeof process !== 'undefined') {
    // For node use HTTP adapter
    adapter = __webpack_require__(/*! ./adapters/http */ "./node_modules/axios/lib/adapters/xhr.js");
  }
  return adapter;
}

var defaults = {
  adapter: getDefaultAdapter(),

  transformRequest: [function transformRequest(data, headers) {
    normalizeHeaderName(headers, 'Content-Type');
    if (utils.isFormData(data) ||
      utils.isArrayBuffer(data) ||
      utils.isBuffer(data) ||
      utils.isStream(data) ||
      utils.isFile(data) ||
      utils.isBlob(data)
    ) {
      return data;
    }
    if (utils.isArrayBufferView(data)) {
      return data.buffer;
    }
    if (utils.isURLSearchParams(data)) {
      setContentTypeIfUnset(headers, 'application/x-www-form-urlencoded;charset=utf-8');
      return data.toString();
    }
    if (utils.isObject(data)) {
      setContentTypeIfUnset(headers, 'application/json;charset=utf-8');
      return JSON.stringify(data);
    }
    return data;
  }],

  transformResponse: [function transformResponse(data) {
    /*eslint no-param-reassign:0*/
    if (typeof data === 'string') {
      try {
        data = JSON.parse(data);
      } catch (e) { /* Ignore */ }
    }
    return data;
  }],

  /**
   * A timeout in milliseconds to abort a request. If set to 0 (default) a
   * timeout is not created.
   */
  timeout: 0,

  xsrfCookieName: 'XSRF-TOKEN',
  xsrfHeaderName: 'X-XSRF-TOKEN',

  maxContentLength: -1,

  validateStatus: function validateStatus(status) {
    return status >= 200 && status < 300;
  }
};

defaults.headers = {
  common: {
    'Accept': 'application/json, text/plain, */*'
  }
};

utils.forEach(['delete', 'get', 'head'], function forEachMethodNoData(method) {
  defaults.headers[method] = {};
});

utils.forEach(['post', 'put', 'patch'], function forEachMethodWithData(method) {
  defaults.headers[method] = utils.merge(DEFAULT_CONTENT_TYPE);
});

module.exports = defaults;

/* WEBPACK VAR INJECTION */}.call(this, __webpack_require__(/*! ./../../process/browser.js */ "./node_modules/process/browser.js")))

/***/ }),

/***/ "./node_modules/axios/lib/helpers/bind.js":
/*!************************************************!*\
  !*** ./node_modules/axios/lib/helpers/bind.js ***!
  \************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


module.exports = function bind(fn, thisArg) {
  return function wrap() {
    var args = new Array(arguments.length);
    for (var i = 0; i < args.length; i++) {
      args[i] = arguments[i];
    }
    return fn.apply(thisArg, args);
  };
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/buildURL.js":
/*!****************************************************!*\
  !*** ./node_modules/axios/lib/helpers/buildURL.js ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

function encode(val) {
  return encodeURIComponent(val).
    replace(/%40/gi, '@').
    replace(/%3A/gi, ':').
    replace(/%24/g, '$').
    replace(/%2C/gi, ',').
    replace(/%20/g, '+').
    replace(/%5B/gi, '[').
    replace(/%5D/gi, ']');
}

/**
 * Build a URL by appending params to the end
 *
 * @param {string} url The base of the url (e.g., http://www.google.com)
 * @param {object} [params] The params to be appended
 * @returns {string} The formatted url
 */
module.exports = function buildURL(url, params, paramsSerializer) {
  /*eslint no-param-reassign:0*/
  if (!params) {
    return url;
  }

  var serializedParams;
  if (paramsSerializer) {
    serializedParams = paramsSerializer(params);
  } else if (utils.isURLSearchParams(params)) {
    serializedParams = params.toString();
  } else {
    var parts = [];

    utils.forEach(params, function serialize(val, key) {
      if (val === null || typeof val === 'undefined') {
        return;
      }

      if (utils.isArray(val)) {
        key = key + '[]';
      } else {
        val = [val];
      }

      utils.forEach(val, function parseValue(v) {
        if (utils.isDate(v)) {
          v = v.toISOString();
        } else if (utils.isObject(v)) {
          v = JSON.stringify(v);
        }
        parts.push(encode(key) + '=' + encode(v));
      });
    });

    serializedParams = parts.join('&');
  }

  if (serializedParams) {
    url += (url.indexOf('?') === -1 ? '?' : '&') + serializedParams;
  }

  return url;
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/combineURLs.js":
/*!*******************************************************!*\
  !*** ./node_modules/axios/lib/helpers/combineURLs.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Creates a new URL by combining the specified URLs
 *
 * @param {string} baseURL The base URL
 * @param {string} relativeURL The relative URL
 * @returns {string} The combined URL
 */
module.exports = function combineURLs(baseURL, relativeURL) {
  return relativeURL
    ? baseURL.replace(/\/+$/, '') + '/' + relativeURL.replace(/^\/+/, '')
    : baseURL;
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/cookies.js":
/*!***************************************************!*\
  !*** ./node_modules/axios/lib/helpers/cookies.js ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

module.exports = (
  utils.isStandardBrowserEnv() ?

  // Standard browser envs support document.cookie
  (function standardBrowserEnv() {
    return {
      write: function write(name, value, expires, path, domain, secure) {
        var cookie = [];
        cookie.push(name + '=' + encodeURIComponent(value));

        if (utils.isNumber(expires)) {
          cookie.push('expires=' + new Date(expires).toGMTString());
        }

        if (utils.isString(path)) {
          cookie.push('path=' + path);
        }

        if (utils.isString(domain)) {
          cookie.push('domain=' + domain);
        }

        if (secure === true) {
          cookie.push('secure');
        }

        document.cookie = cookie.join('; ');
      },

      read: function read(name) {
        var match = document.cookie.match(new RegExp('(^|;\\s*)(' + name + ')=([^;]*)'));
        return (match ? decodeURIComponent(match[3]) : null);
      },

      remove: function remove(name) {
        this.write(name, '', Date.now() - 86400000);
      }
    };
  })() :

  // Non standard browser env (web workers, react-native) lack needed support.
  (function nonStandardBrowserEnv() {
    return {
      write: function write() {},
      read: function read() { return null; },
      remove: function remove() {}
    };
  })()
);


/***/ }),

/***/ "./node_modules/axios/lib/helpers/isAbsoluteURL.js":
/*!*********************************************************!*\
  !*** ./node_modules/axios/lib/helpers/isAbsoluteURL.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Determines whether the specified URL is absolute
 *
 * @param {string} url The URL to test
 * @returns {boolean} True if the specified URL is absolute, otherwise false
 */
module.exports = function isAbsoluteURL(url) {
  // A URL is considered absolute if it begins with "<scheme>://" or "//" (protocol-relative URL).
  // RFC 3986 defines scheme name as a sequence of characters beginning with a letter and followed
  // by any combination of letters, digits, plus, period, or hyphen.
  return /^([a-z][a-z\d\+\-\.]*:)?\/\//i.test(url);
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/isURLSameOrigin.js":
/*!***********************************************************!*\
  !*** ./node_modules/axios/lib/helpers/isURLSameOrigin.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

module.exports = (
  utils.isStandardBrowserEnv() ?

  // Standard browser envs have full support of the APIs needed to test
  // whether the request URL is of the same origin as current location.
  (function standardBrowserEnv() {
    var msie = /(msie|trident)/i.test(navigator.userAgent);
    var urlParsingNode = document.createElement('a');
    var originURL;

    /**
    * Parse a URL to discover it's components
    *
    * @param {String} url The URL to be parsed
    * @returns {Object}
    */
    function resolveURL(url) {
      var href = url;

      if (msie) {
        // IE needs attribute set twice to normalize properties
        urlParsingNode.setAttribute('href', href);
        href = urlParsingNode.href;
      }

      urlParsingNode.setAttribute('href', href);

      // urlParsingNode provides the UrlUtils interface - http://url.spec.whatwg.org/#urlutils
      return {
        href: urlParsingNode.href,
        protocol: urlParsingNode.protocol ? urlParsingNode.protocol.replace(/:$/, '') : '',
        host: urlParsingNode.host,
        search: urlParsingNode.search ? urlParsingNode.search.replace(/^\?/, '') : '',
        hash: urlParsingNode.hash ? urlParsingNode.hash.replace(/^#/, '') : '',
        hostname: urlParsingNode.hostname,
        port: urlParsingNode.port,
        pathname: (urlParsingNode.pathname.charAt(0) === '/') ?
                  urlParsingNode.pathname :
                  '/' + urlParsingNode.pathname
      };
    }

    originURL = resolveURL(window.location.href);

    /**
    * Determine if a URL shares the same origin as the current location
    *
    * @param {String} requestURL The URL to test
    * @returns {boolean} True if URL shares the same origin, otherwise false
    */
    return function isURLSameOrigin(requestURL) {
      var parsed = (utils.isString(requestURL)) ? resolveURL(requestURL) : requestURL;
      return (parsed.protocol === originURL.protocol &&
            parsed.host === originURL.host);
    };
  })() :

  // Non standard browser envs (web workers, react-native) lack needed support.
  (function nonStandardBrowserEnv() {
    return function isURLSameOrigin() {
      return true;
    };
  })()
);


/***/ }),

/***/ "./node_modules/axios/lib/helpers/normalizeHeaderName.js":
/*!***************************************************************!*\
  !*** ./node_modules/axios/lib/helpers/normalizeHeaderName.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ../utils */ "./node_modules/axios/lib/utils.js");

module.exports = function normalizeHeaderName(headers, normalizedName) {
  utils.forEach(headers, function processHeader(value, name) {
    if (name !== normalizedName && name.toUpperCase() === normalizedName.toUpperCase()) {
      headers[normalizedName] = value;
      delete headers[name];
    }
  });
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/parseHeaders.js":
/*!********************************************************!*\
  !*** ./node_modules/axios/lib/helpers/parseHeaders.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var utils = __webpack_require__(/*! ./../utils */ "./node_modules/axios/lib/utils.js");

// Headers whose duplicates are ignored by node
// c.f. https://nodejs.org/api/http.html#http_message_headers
var ignoreDuplicateOf = [
  'age', 'authorization', 'content-length', 'content-type', 'etag',
  'expires', 'from', 'host', 'if-modified-since', 'if-unmodified-since',
  'last-modified', 'location', 'max-forwards', 'proxy-authorization',
  'referer', 'retry-after', 'user-agent'
];

/**
 * Parse headers into an object
 *
 * ```
 * Date: Wed, 27 Aug 2014 08:58:49 GMT
 * Content-Type: application/json
 * Connection: keep-alive
 * Transfer-Encoding: chunked
 * ```
 *
 * @param {String} headers Headers needing to be parsed
 * @returns {Object} Headers parsed into an object
 */
module.exports = function parseHeaders(headers) {
  var parsed = {};
  var key;
  var val;
  var i;

  if (!headers) { return parsed; }

  utils.forEach(headers.split('\n'), function parser(line) {
    i = line.indexOf(':');
    key = utils.trim(line.substr(0, i)).toLowerCase();
    val = utils.trim(line.substr(i + 1));

    if (key) {
      if (parsed[key] && ignoreDuplicateOf.indexOf(key) >= 0) {
        return;
      }
      if (key === 'set-cookie') {
        parsed[key] = (parsed[key] ? parsed[key] : []).concat([val]);
      } else {
        parsed[key] = parsed[key] ? parsed[key] + ', ' + val : val;
      }
    }
  });

  return parsed;
};


/***/ }),

/***/ "./node_modules/axios/lib/helpers/spread.js":
/*!**************************************************!*\
  !*** ./node_modules/axios/lib/helpers/spread.js ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/**
 * Syntactic sugar for invoking a function and expanding an array for arguments.
 *
 * Common use case would be to use `Function.prototype.apply`.
 *
 *  ```js
 *  function f(x, y, z) {}
 *  var args = [1, 2, 3];
 *  f.apply(null, args);
 *  ```
 *
 * With `spread` this example can be re-written.
 *
 *  ```js
 *  spread(function(x, y, z) {})([1, 2, 3]);
 *  ```
 *
 * @param {Function} callback
 * @returns {Function}
 */
module.exports = function spread(callback) {
  return function wrap(arr) {
    return callback.apply(null, arr);
  };
};


/***/ }),

/***/ "./node_modules/axios/lib/utils.js":
/*!*****************************************!*\
  !*** ./node_modules/axios/lib/utils.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


var bind = __webpack_require__(/*! ./helpers/bind */ "./node_modules/axios/lib/helpers/bind.js");
var isBuffer = __webpack_require__(/*! is-buffer */ "./node_modules/is-buffer/index.js");

/*global toString:true*/

// utils is a library of generic helper functions non-specific to axios

var toString = Object.prototype.toString;

/**
 * Determine if a value is an Array
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an Array, otherwise false
 */
function isArray(val) {
  return toString.call(val) === '[object Array]';
}

/**
 * Determine if a value is an ArrayBuffer
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an ArrayBuffer, otherwise false
 */
function isArrayBuffer(val) {
  return toString.call(val) === '[object ArrayBuffer]';
}

/**
 * Determine if a value is a FormData
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an FormData, otherwise false
 */
function isFormData(val) {
  return (typeof FormData !== 'undefined') && (val instanceof FormData);
}

/**
 * Determine if a value is a view on an ArrayBuffer
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a view on an ArrayBuffer, otherwise false
 */
function isArrayBufferView(val) {
  var result;
  if ((typeof ArrayBuffer !== 'undefined') && (ArrayBuffer.isView)) {
    result = ArrayBuffer.isView(val);
  } else {
    result = (val) && (val.buffer) && (val.buffer instanceof ArrayBuffer);
  }
  return result;
}

/**
 * Determine if a value is a String
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a String, otherwise false
 */
function isString(val) {
  return typeof val === 'string';
}

/**
 * Determine if a value is a Number
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Number, otherwise false
 */
function isNumber(val) {
  return typeof val === 'number';
}

/**
 * Determine if a value is undefined
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if the value is undefined, otherwise false
 */
function isUndefined(val) {
  return typeof val === 'undefined';
}

/**
 * Determine if a value is an Object
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is an Object, otherwise false
 */
function isObject(val) {
  return val !== null && typeof val === 'object';
}

/**
 * Determine if a value is a Date
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Date, otherwise false
 */
function isDate(val) {
  return toString.call(val) === '[object Date]';
}

/**
 * Determine if a value is a File
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a File, otherwise false
 */
function isFile(val) {
  return toString.call(val) === '[object File]';
}

/**
 * Determine if a value is a Blob
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Blob, otherwise false
 */
function isBlob(val) {
  return toString.call(val) === '[object Blob]';
}

/**
 * Determine if a value is a Function
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Function, otherwise false
 */
function isFunction(val) {
  return toString.call(val) === '[object Function]';
}

/**
 * Determine if a value is a Stream
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a Stream, otherwise false
 */
function isStream(val) {
  return isObject(val) && isFunction(val.pipe);
}

/**
 * Determine if a value is a URLSearchParams object
 *
 * @param {Object} val The value to test
 * @returns {boolean} True if value is a URLSearchParams object, otherwise false
 */
function isURLSearchParams(val) {
  return typeof URLSearchParams !== 'undefined' && val instanceof URLSearchParams;
}

/**
 * Trim excess whitespace off the beginning and end of a string
 *
 * @param {String} str The String to trim
 * @returns {String} The String freed of excess whitespace
 */
function trim(str) {
  return str.replace(/^\s*/, '').replace(/\s*$/, '');
}

/**
 * Determine if we're running in a standard browser environment
 *
 * This allows axios to run in a web worker, and react-native.
 * Both environments support XMLHttpRequest, but not fully standard globals.
 *
 * web workers:
 *  typeof window -> undefined
 *  typeof document -> undefined
 *
 * react-native:
 *  navigator.product -> 'ReactNative'
 */
function isStandardBrowserEnv() {
  if (typeof navigator !== 'undefined' && navigator.product === 'ReactNative') {
    return false;
  }
  return (
    typeof window !== 'undefined' &&
    typeof document !== 'undefined'
  );
}

/**
 * Iterate over an Array or an Object invoking a function for each item.
 *
 * If `obj` is an Array callback will be called passing
 * the value, index, and complete array for each item.
 *
 * If 'obj' is an Object callback will be called passing
 * the value, key, and complete object for each property.
 *
 * @param {Object|Array} obj The object to iterate
 * @param {Function} fn The callback to invoke for each item
 */
function forEach(obj, fn) {
  // Don't bother if no value provided
  if (obj === null || typeof obj === 'undefined') {
    return;
  }

  // Force an array if not already something iterable
  if (typeof obj !== 'object') {
    /*eslint no-param-reassign:0*/
    obj = [obj];
  }

  if (isArray(obj)) {
    // Iterate over array values
    for (var i = 0, l = obj.length; i < l; i++) {
      fn.call(null, obj[i], i, obj);
    }
  } else {
    // Iterate over object keys
    for (var key in obj) {
      if (Object.prototype.hasOwnProperty.call(obj, key)) {
        fn.call(null, obj[key], key, obj);
      }
    }
  }
}

/**
 * Accepts varargs expecting each argument to be an object, then
 * immutably merges the properties of each object and returns result.
 *
 * When multiple objects contain the same key the later object in
 * the arguments list will take precedence.
 *
 * Example:
 *
 * ```js
 * var result = merge({foo: 123}, {foo: 456});
 * console.log(result.foo); // outputs 456
 * ```
 *
 * @param {Object} obj1 Object to merge
 * @returns {Object} Result of all merge properties
 */
function merge(/* obj1, obj2, obj3, ... */) {
  var result = {};
  function assignValue(val, key) {
    if (typeof result[key] === 'object' && typeof val === 'object') {
      result[key] = merge(result[key], val);
    } else {
      result[key] = val;
    }
  }

  for (var i = 0, l = arguments.length; i < l; i++) {
    forEach(arguments[i], assignValue);
  }
  return result;
}

/**
 * Extends object a by mutably adding to it the properties of object b.
 *
 * @param {Object} a The object to be extended
 * @param {Object} b The object to copy properties from
 * @param {Object} thisArg The object to bind function to
 * @return {Object} The resulting value of object a
 */
function extend(a, b, thisArg) {
  forEach(b, function assignValue(val, key) {
    if (thisArg && typeof val === 'function') {
      a[key] = bind(val, thisArg);
    } else {
      a[key] = val;
    }
  });
  return a;
}

module.exports = {
  isArray: isArray,
  isArrayBuffer: isArrayBuffer,
  isBuffer: isBuffer,
  isFormData: isFormData,
  isArrayBufferView: isArrayBufferView,
  isString: isString,
  isNumber: isNumber,
  isObject: isObject,
  isUndefined: isUndefined,
  isDate: isDate,
  isFile: isFile,
  isBlob: isBlob,
  isFunction: isFunction,
  isStream: isStream,
  isURLSearchParams: isURLSearchParams,
  isStandardBrowserEnv: isStandardBrowserEnv,
  forEach: forEach,
  merge: merge,
  extend: extend,
  trim: trim
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/from.js":
/*!****************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/from.js ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.string.iterator */ "./node_modules/core-js-pure/modules/es.string.iterator.js");
__webpack_require__(/*! ../../modules/es.array.from */ "./node_modules/core-js-pure/modules/es.array.from.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

module.exports = path.Array.from;


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/virtual/concat.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/virtual/concat.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.array.concat */ "./node_modules/core-js-pure/modules/es.array.concat.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('Array').concat;


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/virtual/includes.js":
/*!****************************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/virtual/includes.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.array.includes */ "./node_modules/core-js-pure/modules/es.array.includes.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('Array').includes;


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/virtual/index-of.js":
/*!****************************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/virtual/index-of.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.array.index-of */ "./node_modules/core-js-pure/modules/es.array.index-of.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('Array').indexOf;


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/virtual/last-index-of.js":
/*!*********************************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/virtual/last-index-of.js ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.array.last-index-of */ "./node_modules/core-js-pure/modules/es.array.last-index-of.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('Array').lastIndexOf;


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/virtual/reduce.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/virtual/reduce.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.array.reduce */ "./node_modules/core-js-pure/modules/es.array.reduce.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('Array').reduce;


/***/ }),

/***/ "./node_modules/core-js-pure/es/array/virtual/slice.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/es/array/virtual/slice.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.array.slice */ "./node_modules/core-js-pure/modules/es.array.slice.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('Array').slice;


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/concat.js":
/*!*********************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/concat.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var concat = __webpack_require__(/*! ../array/virtual/concat */ "./node_modules/core-js-pure/es/array/virtual/concat.js");

var ArrayPrototype = Array.prototype;

module.exports = function (it) {
  var own = it.concat;
  return it === ArrayPrototype || (it instanceof Array && own === ArrayPrototype.concat) ? concat : own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/includes.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/includes.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var arrayIncludes = __webpack_require__(/*! ../array/virtual/includes */ "./node_modules/core-js-pure/es/array/virtual/includes.js");
var stringIncludes = __webpack_require__(/*! ../string/virtual/includes */ "./node_modules/core-js-pure/es/string/virtual/includes.js");

var ArrayPrototype = Array.prototype;
var StringPrototype = String.prototype;

module.exports = function (it) {
  var own = it.includes;
  if (it === ArrayPrototype || (it instanceof Array && own === ArrayPrototype.includes)) return arrayIncludes;
  if (typeof it === 'string' || it === StringPrototype || (it instanceof String && own === StringPrototype.includes)) {
    return stringIncludes;
  } return own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/index-of.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/index-of.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var indexOf = __webpack_require__(/*! ../array/virtual/index-of */ "./node_modules/core-js-pure/es/array/virtual/index-of.js");

var ArrayPrototype = Array.prototype;

module.exports = function (it) {
  var own = it.indexOf;
  return it === ArrayPrototype || (it instanceof Array && own === ArrayPrototype.indexOf) ? indexOf : own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/last-index-of.js":
/*!****************************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/last-index-of.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var lastIndexOf = __webpack_require__(/*! ../array/virtual/last-index-of */ "./node_modules/core-js-pure/es/array/virtual/last-index-of.js");

var ArrayPrototype = Array.prototype;

module.exports = function (it) {
  var own = it.lastIndexOf;
  return it === ArrayPrototype || (it instanceof Array && own === ArrayPrototype.lastIndexOf) ? lastIndexOf : own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/reduce.js":
/*!*********************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/reduce.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var reduce = __webpack_require__(/*! ../array/virtual/reduce */ "./node_modules/core-js-pure/es/array/virtual/reduce.js");

var ArrayPrototype = Array.prototype;

module.exports = function (it) {
  var own = it.reduce;
  return it === ArrayPrototype || (it instanceof Array && own === ArrayPrototype.reduce) ? reduce : own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/slice.js":
/*!********************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/slice.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var slice = __webpack_require__(/*! ../array/virtual/slice */ "./node_modules/core-js-pure/es/array/virtual/slice.js");

var ArrayPrototype = Array.prototype;

module.exports = function (it) {
  var own = it.slice;
  return it === ArrayPrototype || (it instanceof Array && own === ArrayPrototype.slice) ? slice : own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/instance/trim.js":
/*!*******************************************************!*\
  !*** ./node_modules/core-js-pure/es/instance/trim.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var trim = __webpack_require__(/*! ../string/virtual/trim */ "./node_modules/core-js-pure/es/string/virtual/trim.js");

var StringPrototype = String.prototype;

module.exports = function (it) {
  var own = it.trim;
  return typeof it === 'string' || it === StringPrototype
    || (it instanceof String && own === StringPrototype.trim) ? trim : own;
};


/***/ }),

/***/ "./node_modules/core-js-pure/es/object/assign.js":
/*!*******************************************************!*\
  !*** ./node_modules/core-js-pure/es/object/assign.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.object.assign */ "./node_modules/core-js-pure/modules/es.object.assign.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

module.exports = path.Object.assign;


/***/ }),

/***/ "./node_modules/core-js-pure/es/object/define-properties.js":
/*!******************************************************************!*\
  !*** ./node_modules/core-js-pure/es/object/define-properties.js ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.object.define-properties */ "./node_modules/core-js-pure/modules/es.object.define-properties.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

var Object = path.Object;

var defineProperties = module.exports = function defineProperties(T, D) {
  return Object.defineProperties(T, D);
};

if (Object.defineProperties.sham) defineProperties.sham = true;


/***/ }),

/***/ "./node_modules/core-js-pure/es/object/get-own-property-descriptor.js":
/*!****************************************************************************!*\
  !*** ./node_modules/core-js-pure/es/object/get-own-property-descriptor.js ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.object.get-own-property-descriptor */ "./node_modules/core-js-pure/modules/es.object.get-own-property-descriptor.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

var Object = path.Object;

var getOwnPropertyDescriptor = module.exports = function getOwnPropertyDescriptor(it, key) {
  return Object.getOwnPropertyDescriptor(it, key);
};

if (Object.getOwnPropertyDescriptor.sham) getOwnPropertyDescriptor.sham = true;


/***/ }),

/***/ "./node_modules/core-js-pure/es/object/get-own-property-descriptors.js":
/*!*****************************************************************************!*\
  !*** ./node_modules/core-js-pure/es/object/get-own-property-descriptors.js ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.object.get-own-property-descriptors */ "./node_modules/core-js-pure/modules/es.object.get-own-property-descriptors.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

module.exports = path.Object.getOwnPropertyDescriptors;


/***/ }),

/***/ "./node_modules/core-js-pure/es/object/get-own-property-symbols.js":
/*!*************************************************************************!*\
  !*** ./node_modules/core-js-pure/es/object/get-own-property-symbols.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.symbol */ "./node_modules/core-js-pure/modules/es.symbol.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

module.exports = path.Object.getOwnPropertySymbols;


/***/ }),

/***/ "./node_modules/core-js-pure/es/object/keys.js":
/*!*****************************************************!*\
  !*** ./node_modules/core-js-pure/es/object/keys.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.object.keys */ "./node_modules/core-js-pure/modules/es.object.keys.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

module.exports = path.Object.keys;


/***/ }),

/***/ "./node_modules/core-js-pure/es/promise/index.js":
/*!*******************************************************!*\
  !*** ./node_modules/core-js-pure/es/promise/index.js ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../modules/es.object.to-string */ "./node_modules/core-js-pure/modules/es.object.to-string.js");
__webpack_require__(/*! ../../modules/es.string.iterator */ "./node_modules/core-js-pure/modules/es.string.iterator.js");
__webpack_require__(/*! ../../modules/web.dom-collections.iterator */ "./node_modules/core-js-pure/modules/web.dom-collections.iterator.js");
__webpack_require__(/*! ../../modules/es.promise */ "./node_modules/core-js-pure/modules/es.promise.js");
__webpack_require__(/*! ../../modules/es.promise.all-settled */ "./node_modules/core-js-pure/modules/es.promise.all-settled.js");
__webpack_require__(/*! ../../modules/es.promise.finally */ "./node_modules/core-js-pure/modules/es.promise.finally.js");
var path = __webpack_require__(/*! ../../internals/path */ "./node_modules/core-js-pure/internals/path.js");

module.exports = path.Promise;


/***/ }),

/***/ "./node_modules/core-js-pure/es/string/virtual/includes.js":
/*!*****************************************************************!*\
  !*** ./node_modules/core-js-pure/es/string/virtual/includes.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.string.includes */ "./node_modules/core-js-pure/modules/es.string.includes.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('String').includes;


/***/ }),

/***/ "./node_modules/core-js-pure/es/string/virtual/trim.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/es/string/virtual/trim.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../../../modules/es.string.trim */ "./node_modules/core-js-pure/modules/es.string.trim.js");
var entryVirtual = __webpack_require__(/*! ../../../internals/entry-virtual */ "./node_modules/core-js-pure/internals/entry-virtual.js");

module.exports = entryVirtual('String').trim;


/***/ }),

/***/ "./node_modules/core-js-pure/features/array/from.js":
/*!**********************************************************!*\
  !*** ./node_modules/core-js-pure/features/array/from.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/array/from */ "./node_modules/core-js-pure/es/array/from.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/features/array/is-array.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/features/array/is-array.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/array/is-array */ "./node_modules/core-js-pure/es/array/is-array.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/features/instance/slice.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/features/instance/slice.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/slice */ "./node_modules/core-js-pure/es/instance/slice.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/features/is-iterable.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/features/is-iterable.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

__webpack_require__(/*! ../modules/web.dom-collections.iterator */ "./node_modules/core-js-pure/modules/web.dom-collections.iterator.js");
__webpack_require__(/*! ../modules/es.string.iterator */ "./node_modules/core-js-pure/modules/es.string.iterator.js");
var isIterable = __webpack_require__(/*! ../internals/is-iterable */ "./node_modules/core-js-pure/internals/is-iterable.js");

module.exports = isIterable;


/***/ }),

/***/ "./node_modules/core-js-pure/features/promise/index.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/features/promise/index.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/promise */ "./node_modules/core-js-pure/es/promise/index.js");
__webpack_require__(/*! ../../modules/esnext.aggregate-error */ "./node_modules/core-js-pure/modules/esnext.aggregate-error.js");
// TODO: Remove from `core-js@4`
__webpack_require__(/*! ../../modules/esnext.promise.all-settled */ "./node_modules/core-js-pure/modules/esnext.promise.all-settled.js");
__webpack_require__(/*! ../../modules/esnext.promise.try */ "./node_modules/core-js-pure/modules/esnext.promise.try.js");
__webpack_require__(/*! ../../modules/esnext.promise.any */ "./node_modules/core-js-pure/modules/esnext.promise.any.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/internals/an-instance.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/an-instance.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (it, Constructor, name) {
  if (!(it instanceof Constructor)) {
    throw TypeError('Incorrect ' + (name ? name + ' ' : '') + 'invocation');
  } return it;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/array-from.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/internals/array-from.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var bind = __webpack_require__(/*! ../internals/function-bind-context */ "./node_modules/core-js-pure/internals/function-bind-context.js");
var toObject = __webpack_require__(/*! ../internals/to-object */ "./node_modules/core-js-pure/internals/to-object.js");
var callWithSafeIterationClosing = __webpack_require__(/*! ../internals/call-with-safe-iteration-closing */ "./node_modules/core-js-pure/internals/call-with-safe-iteration-closing.js");
var isArrayIteratorMethod = __webpack_require__(/*! ../internals/is-array-iterator-method */ "./node_modules/core-js-pure/internals/is-array-iterator-method.js");
var toLength = __webpack_require__(/*! ../internals/to-length */ "./node_modules/core-js-pure/internals/to-length.js");
var createProperty = __webpack_require__(/*! ../internals/create-property */ "./node_modules/core-js-pure/internals/create-property.js");
var getIteratorMethod = __webpack_require__(/*! ../internals/get-iterator-method */ "./node_modules/core-js-pure/internals/get-iterator-method.js");

// `Array.from` method implementation
// https://tc39.github.io/ecma262/#sec-array.from
module.exports = function from(arrayLike /* , mapfn = undefined, thisArg = undefined */) {
  var O = toObject(arrayLike);
  var C = typeof this == 'function' ? this : Array;
  var argumentsLength = arguments.length;
  var mapfn = argumentsLength > 1 ? arguments[1] : undefined;
  var mapping = mapfn !== undefined;
  var iteratorMethod = getIteratorMethod(O);
  var index = 0;
  var length, result, step, iterator, next, value;
  if (mapping) mapfn = bind(mapfn, argumentsLength > 2 ? arguments[2] : undefined, 2);
  // if the target is not iterable or it's an array with the default iterator - use a simple case
  if (iteratorMethod != undefined && !(C == Array && isArrayIteratorMethod(iteratorMethod))) {
    iterator = iteratorMethod.call(O);
    next = iterator.next;
    result = new C();
    for (;!(step = next.call(iterator)).done; index++) {
      value = mapping ? callWithSafeIterationClosing(iterator, mapfn, [step.value, index], true) : step.value;
      createProperty(result, index, value);
    }
  } else {
    length = toLength(O.length);
    result = new C(length);
    for (;length > index; index++) {
      value = mapping ? mapfn(O[index], index) : O[index];
      createProperty(result, index, value);
    }
  }
  result.length = index;
  return result;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/array-last-index-of.js":
/*!********************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/array-last-index-of.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var toIndexedObject = __webpack_require__(/*! ../internals/to-indexed-object */ "./node_modules/core-js-pure/internals/to-indexed-object.js");
var toInteger = __webpack_require__(/*! ../internals/to-integer */ "./node_modules/core-js-pure/internals/to-integer.js");
var toLength = __webpack_require__(/*! ../internals/to-length */ "./node_modules/core-js-pure/internals/to-length.js");
var arrayMethodIsStrict = __webpack_require__(/*! ../internals/array-method-is-strict */ "./node_modules/core-js-pure/internals/array-method-is-strict.js");
var arrayMethodUsesToLength = __webpack_require__(/*! ../internals/array-method-uses-to-length */ "./node_modules/core-js-pure/internals/array-method-uses-to-length.js");

var min = Math.min;
var nativeLastIndexOf = [].lastIndexOf;
var NEGATIVE_ZERO = !!nativeLastIndexOf && 1 / [1].lastIndexOf(1, -0) < 0;
var STRICT_METHOD = arrayMethodIsStrict('lastIndexOf');
// For preventing possible almost infinite loop in non-standard implementations, test the forward version of the method
var USES_TO_LENGTH = arrayMethodUsesToLength('indexOf', { ACCESSORS: true, 1: 0 });
var FORCED = NEGATIVE_ZERO || !STRICT_METHOD || !USES_TO_LENGTH;

// `Array.prototype.lastIndexOf` method implementation
// https://tc39.github.io/ecma262/#sec-array.prototype.lastindexof
module.exports = FORCED ? function lastIndexOf(searchElement /* , fromIndex = @[*-1] */) {
  // convert -0 to +0
  if (NEGATIVE_ZERO) return nativeLastIndexOf.apply(this, arguments) || 0;
  var O = toIndexedObject(this);
  var length = toLength(O.length);
  var index = length - 1;
  if (arguments.length > 1) index = min(index, toInteger(arguments[1]));
  if (index < 0) index = length + index;
  for (;index >= 0; index--) if (index in O && O[index] === searchElement) return index || 0;
  return -1;
} : nativeLastIndexOf;


/***/ }),

/***/ "./node_modules/core-js-pure/internals/array-reduce.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/array-reduce.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var aFunction = __webpack_require__(/*! ../internals/a-function */ "./node_modules/core-js-pure/internals/a-function.js");
var toObject = __webpack_require__(/*! ../internals/to-object */ "./node_modules/core-js-pure/internals/to-object.js");
var IndexedObject = __webpack_require__(/*! ../internals/indexed-object */ "./node_modules/core-js-pure/internals/indexed-object.js");
var toLength = __webpack_require__(/*! ../internals/to-length */ "./node_modules/core-js-pure/internals/to-length.js");

// `Array.prototype.{ reduce, reduceRight }` methods implementation
var createMethod = function (IS_RIGHT) {
  return function (that, callbackfn, argumentsLength, memo) {
    aFunction(callbackfn);
    var O = toObject(that);
    var self = IndexedObject(O);
    var length = toLength(O.length);
    var index = IS_RIGHT ? length - 1 : 0;
    var i = IS_RIGHT ? -1 : 1;
    if (argumentsLength < 2) while (true) {
      if (index in self) {
        memo = self[index];
        index += i;
        break;
      }
      index += i;
      if (IS_RIGHT ? index < 0 : length <= index) {
        throw TypeError('Reduce of empty array with no initial value');
      }
    }
    for (;IS_RIGHT ? index >= 0 : length > index; index += i) if (index in self) {
      memo = callbackfn(memo, self[index], index, O);
    }
    return memo;
  };
};

module.exports = {
  // `Array.prototype.reduce` method
  // https://tc39.github.io/ecma262/#sec-array.prototype.reduce
  left: createMethod(false),
  // `Array.prototype.reduceRight` method
  // https://tc39.github.io/ecma262/#sec-array.prototype.reduceright
  right: createMethod(true)
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/call-with-safe-iteration-closing.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/call-with-safe-iteration-closing.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ../internals/an-object */ "./node_modules/core-js-pure/internals/an-object.js");

// call something on iterator step with safe closing on error
module.exports = function (iterator, fn, value, ENTRIES) {
  try {
    return ENTRIES ? fn(anObject(value)[0], value[1]) : fn(value);
  // 7.4.6 IteratorClose(iterator, completion)
  } catch (error) {
    var returnMethod = iterator['return'];
    if (returnMethod !== undefined) anObject(returnMethod.call(iterator));
    throw error;
  }
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/check-correctness-of-iteration.js":
/*!*******************************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/check-correctness-of-iteration.js ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");

var ITERATOR = wellKnownSymbol('iterator');
var SAFE_CLOSING = false;

try {
  var called = 0;
  var iteratorWithReturn = {
    next: function () {
      return { done: !!called++ };
    },
    'return': function () {
      SAFE_CLOSING = true;
    }
  };
  iteratorWithReturn[ITERATOR] = function () {
    return this;
  };
  // eslint-disable-next-line no-throw-literal
  Array.from(iteratorWithReturn, function () { throw 2; });
} catch (error) { /* empty */ }

module.exports = function (exec, SKIP_CLOSING) {
  if (!SKIP_CLOSING && !SAFE_CLOSING) return false;
  var ITERATION_SUPPORT = false;
  try {
    var object = {};
    object[ITERATOR] = function () {
      return {
        next: function () {
          return { done: ITERATION_SUPPORT = true };
        }
      };
    };
    exec(object);
  } catch (error) { /* empty */ }
  return ITERATION_SUPPORT;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/correct-is-regexp-logic.js":
/*!************************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/correct-is-regexp-logic.js ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");

var MATCH = wellKnownSymbol('match');

module.exports = function (METHOD_NAME) {
  var regexp = /./;
  try {
    '/./'[METHOD_NAME](regexp);
  } catch (e) {
    try {
      regexp[MATCH] = false;
      return '/./'[METHOD_NAME](regexp);
    } catch (f) { /* empty */ }
  } return false;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/engine-is-ios.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/engine-is-ios.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var userAgent = __webpack_require__(/*! ../internals/engine-user-agent */ "./node_modules/core-js-pure/internals/engine-user-agent.js");

module.exports = /(iphone|ipod|ipad).*applewebkit/i.test(userAgent);


/***/ }),

/***/ "./node_modules/core-js-pure/internals/get-iterator-method.js":
/*!********************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/get-iterator-method.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var classof = __webpack_require__(/*! ../internals/classof */ "./node_modules/core-js-pure/internals/classof.js");
var Iterators = __webpack_require__(/*! ../internals/iterators */ "./node_modules/core-js-pure/internals/iterators.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");

var ITERATOR = wellKnownSymbol('iterator');

module.exports = function (it) {
  if (it != undefined) return it[ITERATOR]
    || it['@@iterator']
    || Iterators[classof(it)];
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/host-report-errors.js":
/*!*******************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/host-report-errors.js ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ../internals/global */ "./node_modules/core-js-pure/internals/global.js");

module.exports = function (a, b) {
  var console = global.console;
  if (console && console.error) {
    arguments.length === 1 ? console.error(a) : console.error(a, b);
  }
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/is-array-iterator-method.js":
/*!*************************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/is-array-iterator-method.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");
var Iterators = __webpack_require__(/*! ../internals/iterators */ "./node_modules/core-js-pure/internals/iterators.js");

var ITERATOR = wellKnownSymbol('iterator');
var ArrayPrototype = Array.prototype;

// check on default Array iterator
module.exports = function (it) {
  return it !== undefined && (Iterators.Array === it || ArrayPrototype[ITERATOR] === it);
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/is-iterable.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/is-iterable.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var classof = __webpack_require__(/*! ../internals/classof */ "./node_modules/core-js-pure/internals/classof.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");
var Iterators = __webpack_require__(/*! ../internals/iterators */ "./node_modules/core-js-pure/internals/iterators.js");

var ITERATOR = wellKnownSymbol('iterator');

module.exports = function (it) {
  var O = Object(it);
  return O[ITERATOR] !== undefined
    || '@@iterator' in O
    // eslint-disable-next-line no-prototype-builtins
    || Iterators.hasOwnProperty(classof(O));
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/is-regexp.js":
/*!**********************************************************!*\
  !*** ./node_modules/core-js-pure/internals/is-regexp.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var isObject = __webpack_require__(/*! ../internals/is-object */ "./node_modules/core-js-pure/internals/is-object.js");
var classof = __webpack_require__(/*! ../internals/classof-raw */ "./node_modules/core-js-pure/internals/classof-raw.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");

var MATCH = wellKnownSymbol('match');

// `IsRegExp` abstract operation
// https://tc39.github.io/ecma262/#sec-isregexp
module.exports = function (it) {
  var isRegExp;
  return isObject(it) && ((isRegExp = it[MATCH]) !== undefined ? !!isRegExp : classof(it) == 'RegExp');
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/iterate.js":
/*!********************************************************!*\
  !*** ./node_modules/core-js-pure/internals/iterate.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ../internals/an-object */ "./node_modules/core-js-pure/internals/an-object.js");
var isArrayIteratorMethod = __webpack_require__(/*! ../internals/is-array-iterator-method */ "./node_modules/core-js-pure/internals/is-array-iterator-method.js");
var toLength = __webpack_require__(/*! ../internals/to-length */ "./node_modules/core-js-pure/internals/to-length.js");
var bind = __webpack_require__(/*! ../internals/function-bind-context */ "./node_modules/core-js-pure/internals/function-bind-context.js");
var getIteratorMethod = __webpack_require__(/*! ../internals/get-iterator-method */ "./node_modules/core-js-pure/internals/get-iterator-method.js");
var callWithSafeIterationClosing = __webpack_require__(/*! ../internals/call-with-safe-iteration-closing */ "./node_modules/core-js-pure/internals/call-with-safe-iteration-closing.js");

var Result = function (stopped, result) {
  this.stopped = stopped;
  this.result = result;
};

var iterate = module.exports = function (iterable, fn, that, AS_ENTRIES, IS_ITERATOR) {
  var boundFunction = bind(fn, that, AS_ENTRIES ? 2 : 1);
  var iterator, iterFn, index, length, result, next, step;

  if (IS_ITERATOR) {
    iterator = iterable;
  } else {
    iterFn = getIteratorMethod(iterable);
    if (typeof iterFn != 'function') throw TypeError('Target is not iterable');
    // optimisation for array iterators
    if (isArrayIteratorMethod(iterFn)) {
      for (index = 0, length = toLength(iterable.length); length > index; index++) {
        result = AS_ENTRIES
          ? boundFunction(anObject(step = iterable[index])[0], step[1])
          : boundFunction(iterable[index]);
        if (result && result instanceof Result) return result;
      } return new Result(false);
    }
    iterator = iterFn.call(iterable);
  }

  next = iterator.next;
  while (!(step = next.call(iterator)).done) {
    result = callWithSafeIterationClosing(iterator, boundFunction, step.value, AS_ENTRIES);
    if (typeof result == 'object' && result && result instanceof Result) return result;
  } return new Result(false);
};

iterate.stop = function (result) {
  return new Result(true, result);
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/microtask.js":
/*!**********************************************************!*\
  !*** ./node_modules/core-js-pure/internals/microtask.js ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ../internals/global */ "./node_modules/core-js-pure/internals/global.js");
var getOwnPropertyDescriptor = __webpack_require__(/*! ../internals/object-get-own-property-descriptor */ "./node_modules/core-js-pure/internals/object-get-own-property-descriptor.js").f;
var classof = __webpack_require__(/*! ../internals/classof-raw */ "./node_modules/core-js-pure/internals/classof-raw.js");
var macrotask = __webpack_require__(/*! ../internals/task */ "./node_modules/core-js-pure/internals/task.js").set;
var IS_IOS = __webpack_require__(/*! ../internals/engine-is-ios */ "./node_modules/core-js-pure/internals/engine-is-ios.js");

var MutationObserver = global.MutationObserver || global.WebKitMutationObserver;
var process = global.process;
var Promise = global.Promise;
var IS_NODE = classof(process) == 'process';
// Node.js 11 shows ExperimentalWarning on getting `queueMicrotask`
var queueMicrotaskDescriptor = getOwnPropertyDescriptor(global, 'queueMicrotask');
var queueMicrotask = queueMicrotaskDescriptor && queueMicrotaskDescriptor.value;

var flush, head, last, notify, toggle, node, promise, then;

// modern engines have queueMicrotask method
if (!queueMicrotask) {
  flush = function () {
    var parent, fn;
    if (IS_NODE && (parent = process.domain)) parent.exit();
    while (head) {
      fn = head.fn;
      head = head.next;
      try {
        fn();
      } catch (error) {
        if (head) notify();
        else last = undefined;
        throw error;
      }
    } last = undefined;
    if (parent) parent.enter();
  };

  // Node.js
  if (IS_NODE) {
    notify = function () {
      process.nextTick(flush);
    };
  // browsers with MutationObserver, except iOS - https://github.com/zloirock/core-js/issues/339
  } else if (MutationObserver && !IS_IOS) {
    toggle = true;
    node = document.createTextNode('');
    new MutationObserver(flush).observe(node, { characterData: true });
    notify = function () {
      node.data = toggle = !toggle;
    };
  // environments with maybe non-completely correct, but existent Promise
  } else if (Promise && Promise.resolve) {
    // Promise.resolve without an argument throws an error in LG WebOS 2
    promise = Promise.resolve(undefined);
    then = promise.then;
    notify = function () {
      then.call(promise, flush);
    };
  // for other environments - macrotask based on:
  // - setImmediate
  // - MessageChannel
  // - window.postMessag
  // - onreadystatechange
  // - setTimeout
  } else {
    notify = function () {
      // strange IE + webpack dev server bug - use .call(global)
      macrotask.call(global, flush);
    };
  }
}

module.exports = queueMicrotask || function (fn) {
  var task = { fn: fn, next: undefined };
  if (last) last.next = task;
  if (!head) {
    head = task;
    notify();
  } last = task;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/native-promise-constructor.js":
/*!***************************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/native-promise-constructor.js ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ../internals/global */ "./node_modules/core-js-pure/internals/global.js");

module.exports = global.Promise;


/***/ }),

/***/ "./node_modules/core-js-pure/internals/new-promise-capability.js":
/*!***********************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/new-promise-capability.js ***!
  \***********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var aFunction = __webpack_require__(/*! ../internals/a-function */ "./node_modules/core-js-pure/internals/a-function.js");

var PromiseCapability = function (C) {
  var resolve, reject;
  this.promise = new C(function ($$resolve, $$reject) {
    if (resolve !== undefined || reject !== undefined) throw TypeError('Bad Promise constructor');
    resolve = $$resolve;
    reject = $$reject;
  });
  this.resolve = aFunction(resolve);
  this.reject = aFunction(reject);
};

// 25.4.1.5 NewPromiseCapability(C)
module.exports.f = function (C) {
  return new PromiseCapability(C);
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/not-a-regexp.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/not-a-regexp.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var isRegExp = __webpack_require__(/*! ../internals/is-regexp */ "./node_modules/core-js-pure/internals/is-regexp.js");

module.exports = function (it) {
  if (isRegExp(it)) {
    throw TypeError("The method doesn't accept regular expressions");
  } return it;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/object-assign.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/object-assign.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var DESCRIPTORS = __webpack_require__(/*! ../internals/descriptors */ "./node_modules/core-js-pure/internals/descriptors.js");
var fails = __webpack_require__(/*! ../internals/fails */ "./node_modules/core-js-pure/internals/fails.js");
var objectKeys = __webpack_require__(/*! ../internals/object-keys */ "./node_modules/core-js-pure/internals/object-keys.js");
var getOwnPropertySymbolsModule = __webpack_require__(/*! ../internals/object-get-own-property-symbols */ "./node_modules/core-js-pure/internals/object-get-own-property-symbols.js");
var propertyIsEnumerableModule = __webpack_require__(/*! ../internals/object-property-is-enumerable */ "./node_modules/core-js-pure/internals/object-property-is-enumerable.js");
var toObject = __webpack_require__(/*! ../internals/to-object */ "./node_modules/core-js-pure/internals/to-object.js");
var IndexedObject = __webpack_require__(/*! ../internals/indexed-object */ "./node_modules/core-js-pure/internals/indexed-object.js");

var nativeAssign = Object.assign;
var defineProperty = Object.defineProperty;

// `Object.assign` method
// https://tc39.github.io/ecma262/#sec-object.assign
module.exports = !nativeAssign || fails(function () {
  // should have correct order of operations (Edge bug)
  if (DESCRIPTORS && nativeAssign({ b: 1 }, nativeAssign(defineProperty({}, 'a', {
    enumerable: true,
    get: function () {
      defineProperty(this, 'b', {
        value: 3,
        enumerable: false
      });
    }
  }), { b: 2 })).b !== 1) return true;
  // should work with symbols and should have deterministic property order (V8 bug)
  var A = {};
  var B = {};
  // eslint-disable-next-line no-undef
  var symbol = Symbol();
  var alphabet = 'abcdefghijklmnopqrst';
  A[symbol] = 7;
  alphabet.split('').forEach(function (chr) { B[chr] = chr; });
  return nativeAssign({}, A)[symbol] != 7 || objectKeys(nativeAssign({}, B)).join('') != alphabet;
}) ? function assign(target, source) { // eslint-disable-line no-unused-vars
  var T = toObject(target);
  var argumentsLength = arguments.length;
  var index = 1;
  var getOwnPropertySymbols = getOwnPropertySymbolsModule.f;
  var propertyIsEnumerable = propertyIsEnumerableModule.f;
  while (argumentsLength > index) {
    var S = IndexedObject(arguments[index++]);
    var keys = getOwnPropertySymbols ? objectKeys(S).concat(getOwnPropertySymbols(S)) : objectKeys(S);
    var length = keys.length;
    var j = 0;
    var key;
    while (length > j) {
      key = keys[j++];
      if (!DESCRIPTORS || propertyIsEnumerable.call(S, key)) T[key] = S[key];
    }
  } return T;
} : nativeAssign;


/***/ }),

/***/ "./node_modules/core-js-pure/internals/own-keys.js":
/*!*********************************************************!*\
  !*** ./node_modules/core-js-pure/internals/own-keys.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var getBuiltIn = __webpack_require__(/*! ../internals/get-built-in */ "./node_modules/core-js-pure/internals/get-built-in.js");
var getOwnPropertyNamesModule = __webpack_require__(/*! ../internals/object-get-own-property-names */ "./node_modules/core-js-pure/internals/object-get-own-property-names.js");
var getOwnPropertySymbolsModule = __webpack_require__(/*! ../internals/object-get-own-property-symbols */ "./node_modules/core-js-pure/internals/object-get-own-property-symbols.js");
var anObject = __webpack_require__(/*! ../internals/an-object */ "./node_modules/core-js-pure/internals/an-object.js");

// all object keys, includes non-enumerable and symbols
module.exports = getBuiltIn('Reflect', 'ownKeys') || function ownKeys(it) {
  var keys = getOwnPropertyNamesModule.f(anObject(it));
  var getOwnPropertySymbols = getOwnPropertySymbolsModule.f;
  return getOwnPropertySymbols ? keys.concat(getOwnPropertySymbols(it)) : keys;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/perform.js":
/*!********************************************************!*\
  !*** ./node_modules/core-js-pure/internals/perform.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = function (exec) {
  try {
    return { error: false, value: exec() };
  } catch (error) {
    return { error: true, value: error };
  }
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/promise-resolve.js":
/*!****************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/promise-resolve.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ../internals/an-object */ "./node_modules/core-js-pure/internals/an-object.js");
var isObject = __webpack_require__(/*! ../internals/is-object */ "./node_modules/core-js-pure/internals/is-object.js");
var newPromiseCapability = __webpack_require__(/*! ../internals/new-promise-capability */ "./node_modules/core-js-pure/internals/new-promise-capability.js");

module.exports = function (C, x) {
  anObject(C);
  if (isObject(x) && x.constructor === C) return x;
  var promiseCapability = newPromiseCapability.f(C);
  var resolve = promiseCapability.resolve;
  resolve(x);
  return promiseCapability.promise;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/redefine-all.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/redefine-all.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var redefine = __webpack_require__(/*! ../internals/redefine */ "./node_modules/core-js-pure/internals/redefine.js");

module.exports = function (target, src, options) {
  for (var key in src) {
    if (options && options.unsafe && target[key]) target[key] = src[key];
    else redefine(target, key, src[key], options);
  } return target;
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/set-species.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/set-species.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var getBuiltIn = __webpack_require__(/*! ../internals/get-built-in */ "./node_modules/core-js-pure/internals/get-built-in.js");
var definePropertyModule = __webpack_require__(/*! ../internals/object-define-property */ "./node_modules/core-js-pure/internals/object-define-property.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");
var DESCRIPTORS = __webpack_require__(/*! ../internals/descriptors */ "./node_modules/core-js-pure/internals/descriptors.js");

var SPECIES = wellKnownSymbol('species');

module.exports = function (CONSTRUCTOR_NAME) {
  var Constructor = getBuiltIn(CONSTRUCTOR_NAME);
  var defineProperty = definePropertyModule.f;

  if (DESCRIPTORS && Constructor && !Constructor[SPECIES]) {
    defineProperty(Constructor, SPECIES, {
      configurable: true,
      get: function () { return this; }
    });
  }
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/species-constructor.js":
/*!********************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/species-constructor.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var anObject = __webpack_require__(/*! ../internals/an-object */ "./node_modules/core-js-pure/internals/an-object.js");
var aFunction = __webpack_require__(/*! ../internals/a-function */ "./node_modules/core-js-pure/internals/a-function.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");

var SPECIES = wellKnownSymbol('species');

// `SpeciesConstructor` abstract operation
// https://tc39.github.io/ecma262/#sec-speciesconstructor
module.exports = function (O, defaultConstructor) {
  var C = anObject(O).constructor;
  var S;
  return C === undefined || (S = anObject(C)[SPECIES]) == undefined ? defaultConstructor : aFunction(S);
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/string-trim-forced.js":
/*!*******************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/string-trim-forced.js ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var fails = __webpack_require__(/*! ../internals/fails */ "./node_modules/core-js-pure/internals/fails.js");
var whitespaces = __webpack_require__(/*! ../internals/whitespaces */ "./node_modules/core-js-pure/internals/whitespaces.js");

var non = '\u200B\u0085\u180E';

// check that a method works with the correct list
// of whitespaces and has a correct name
module.exports = function (METHOD_NAME) {
  return fails(function () {
    return !!whitespaces[METHOD_NAME]() || non[METHOD_NAME]() != non || whitespaces[METHOD_NAME].name !== METHOD_NAME;
  });
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/string-trim.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/string-trim.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var requireObjectCoercible = __webpack_require__(/*! ../internals/require-object-coercible */ "./node_modules/core-js-pure/internals/require-object-coercible.js");
var whitespaces = __webpack_require__(/*! ../internals/whitespaces */ "./node_modules/core-js-pure/internals/whitespaces.js");

var whitespace = '[' + whitespaces + ']';
var ltrim = RegExp('^' + whitespace + whitespace + '*');
var rtrim = RegExp(whitespace + whitespace + '*$');

// `String.prototype.{ trim, trimStart, trimEnd, trimLeft, trimRight }` methods implementation
var createMethod = function (TYPE) {
  return function ($this) {
    var string = String(requireObjectCoercible($this));
    if (TYPE & 1) string = string.replace(ltrim, '');
    if (TYPE & 2) string = string.replace(rtrim, '');
    return string;
  };
};

module.exports = {
  // `String.prototype.{ trimLeft, trimStart }` methods
  // https://tc39.github.io/ecma262/#sec-string.prototype.trimstart
  start: createMethod(1),
  // `String.prototype.{ trimRight, trimEnd }` methods
  // https://tc39.github.io/ecma262/#sec-string.prototype.trimend
  end: createMethod(2),
  // `String.prototype.trim` method
  // https://tc39.github.io/ecma262/#sec-string.prototype.trim
  trim: createMethod(3)
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/task.js":
/*!*****************************************************!*\
  !*** ./node_modules/core-js-pure/internals/task.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var global = __webpack_require__(/*! ../internals/global */ "./node_modules/core-js-pure/internals/global.js");
var fails = __webpack_require__(/*! ../internals/fails */ "./node_modules/core-js-pure/internals/fails.js");
var classof = __webpack_require__(/*! ../internals/classof-raw */ "./node_modules/core-js-pure/internals/classof-raw.js");
var bind = __webpack_require__(/*! ../internals/function-bind-context */ "./node_modules/core-js-pure/internals/function-bind-context.js");
var html = __webpack_require__(/*! ../internals/html */ "./node_modules/core-js-pure/internals/html.js");
var createElement = __webpack_require__(/*! ../internals/document-create-element */ "./node_modules/core-js-pure/internals/document-create-element.js");
var IS_IOS = __webpack_require__(/*! ../internals/engine-is-ios */ "./node_modules/core-js-pure/internals/engine-is-ios.js");

var location = global.location;
var set = global.setImmediate;
var clear = global.clearImmediate;
var process = global.process;
var MessageChannel = global.MessageChannel;
var Dispatch = global.Dispatch;
var counter = 0;
var queue = {};
var ONREADYSTATECHANGE = 'onreadystatechange';
var defer, channel, port;

var run = function (id) {
  // eslint-disable-next-line no-prototype-builtins
  if (queue.hasOwnProperty(id)) {
    var fn = queue[id];
    delete queue[id];
    fn();
  }
};

var runner = function (id) {
  return function () {
    run(id);
  };
};

var listener = function (event) {
  run(event.data);
};

var post = function (id) {
  // old engines have not location.origin
  global.postMessage(id + '', location.protocol + '//' + location.host);
};

// Node.js 0.9+ & IE10+ has setImmediate, otherwise:
if (!set || !clear) {
  set = function setImmediate(fn) {
    var args = [];
    var i = 1;
    while (arguments.length > i) args.push(arguments[i++]);
    queue[++counter] = function () {
      // eslint-disable-next-line no-new-func
      (typeof fn == 'function' ? fn : Function(fn)).apply(undefined, args);
    };
    defer(counter);
    return counter;
  };
  clear = function clearImmediate(id) {
    delete queue[id];
  };
  // Node.js 0.8-
  if (classof(process) == 'process') {
    defer = function (id) {
      process.nextTick(runner(id));
    };
  // Sphere (JS game engine) Dispatch API
  } else if (Dispatch && Dispatch.now) {
    defer = function (id) {
      Dispatch.now(runner(id));
    };
  // Browsers with MessageChannel, includes WebWorkers
  // except iOS - https://github.com/zloirock/core-js/issues/624
  } else if (MessageChannel && !IS_IOS) {
    channel = new MessageChannel();
    port = channel.port2;
    channel.port1.onmessage = listener;
    defer = bind(port.postMessage, port, 1);
  // Browsers with postMessage, skip WebWorkers
  // IE8 has postMessage, but it's sync & typeof its postMessage is 'object'
  } else if (
    global.addEventListener &&
    typeof postMessage == 'function' &&
    !global.importScripts &&
    !fails(post) &&
    location.protocol !== 'file:'
  ) {
    defer = post;
    global.addEventListener('message', listener, false);
  // IE8-
  } else if (ONREADYSTATECHANGE in createElement('script')) {
    defer = function (id) {
      html.appendChild(createElement('script'))[ONREADYSTATECHANGE] = function () {
        html.removeChild(this);
        run(id);
      };
    };
  // Rest old browsers
  } else {
    defer = function (id) {
      setTimeout(runner(id), 0);
    };
  }
}

module.exports = {
  set: set,
  clear: clear
};


/***/ }),

/***/ "./node_modules/core-js-pure/internals/whitespaces.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/internals/whitespaces.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// a string of all valid unicode whitespaces
// eslint-disable-next-line max-len
module.exports = '\u0009\u000A\u000B\u000C\u000D\u0020\u00A0\u1680\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200A\u202F\u205F\u3000\u2028\u2029\uFEFF';


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.array.from.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.array.from.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var from = __webpack_require__(/*! ../internals/array-from */ "./node_modules/core-js-pure/internals/array-from.js");
var checkCorrectnessOfIteration = __webpack_require__(/*! ../internals/check-correctness-of-iteration */ "./node_modules/core-js-pure/internals/check-correctness-of-iteration.js");

var INCORRECT_ITERATION = !checkCorrectnessOfIteration(function (iterable) {
  Array.from(iterable);
});

// `Array.from` method
// https://tc39.github.io/ecma262/#sec-array.from
$({ target: 'Array', stat: true, forced: INCORRECT_ITERATION }, {
  from: from
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.array.includes.js":
/*!****************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.array.includes.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var $includes = __webpack_require__(/*! ../internals/array-includes */ "./node_modules/core-js-pure/internals/array-includes.js").includes;
var addToUnscopables = __webpack_require__(/*! ../internals/add-to-unscopables */ "./node_modules/core-js-pure/internals/add-to-unscopables.js");
var arrayMethodUsesToLength = __webpack_require__(/*! ../internals/array-method-uses-to-length */ "./node_modules/core-js-pure/internals/array-method-uses-to-length.js");

var USES_TO_LENGTH = arrayMethodUsesToLength('indexOf', { ACCESSORS: true, 1: 0 });

// `Array.prototype.includes` method
// https://tc39.github.io/ecma262/#sec-array.prototype.includes
$({ target: 'Array', proto: true, forced: !USES_TO_LENGTH }, {
  includes: function includes(el /* , fromIndex = 0 */) {
    return $includes(this, el, arguments.length > 1 ? arguments[1] : undefined);
  }
});

// https://tc39.github.io/ecma262/#sec-array.prototype-@@unscopables
addToUnscopables('includes');


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.array.index-of.js":
/*!****************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.array.index-of.js ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var $indexOf = __webpack_require__(/*! ../internals/array-includes */ "./node_modules/core-js-pure/internals/array-includes.js").indexOf;
var arrayMethodIsStrict = __webpack_require__(/*! ../internals/array-method-is-strict */ "./node_modules/core-js-pure/internals/array-method-is-strict.js");
var arrayMethodUsesToLength = __webpack_require__(/*! ../internals/array-method-uses-to-length */ "./node_modules/core-js-pure/internals/array-method-uses-to-length.js");

var nativeIndexOf = [].indexOf;

var NEGATIVE_ZERO = !!nativeIndexOf && 1 / [1].indexOf(1, -0) < 0;
var STRICT_METHOD = arrayMethodIsStrict('indexOf');
var USES_TO_LENGTH = arrayMethodUsesToLength('indexOf', { ACCESSORS: true, 1: 0 });

// `Array.prototype.indexOf` method
// https://tc39.github.io/ecma262/#sec-array.prototype.indexof
$({ target: 'Array', proto: true, forced: NEGATIVE_ZERO || !STRICT_METHOD || !USES_TO_LENGTH }, {
  indexOf: function indexOf(searchElement /* , fromIndex = 0 */) {
    return NEGATIVE_ZERO
      // convert -0 to +0
      ? nativeIndexOf.apply(this, arguments) || 0
      : $indexOf(this, searchElement, arguments.length > 1 ? arguments[1] : undefined);
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.array.last-index-of.js":
/*!*********************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.array.last-index-of.js ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var lastIndexOf = __webpack_require__(/*! ../internals/array-last-index-of */ "./node_modules/core-js-pure/internals/array-last-index-of.js");

// `Array.prototype.lastIndexOf` method
// https://tc39.github.io/ecma262/#sec-array.prototype.lastindexof
$({ target: 'Array', proto: true, forced: lastIndexOf !== [].lastIndexOf }, {
  lastIndexOf: lastIndexOf
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.array.reduce.js":
/*!**************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.array.reduce.js ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var $reduce = __webpack_require__(/*! ../internals/array-reduce */ "./node_modules/core-js-pure/internals/array-reduce.js").left;
var arrayMethodIsStrict = __webpack_require__(/*! ../internals/array-method-is-strict */ "./node_modules/core-js-pure/internals/array-method-is-strict.js");
var arrayMethodUsesToLength = __webpack_require__(/*! ../internals/array-method-uses-to-length */ "./node_modules/core-js-pure/internals/array-method-uses-to-length.js");

var STRICT_METHOD = arrayMethodIsStrict('reduce');
var USES_TO_LENGTH = arrayMethodUsesToLength('reduce', { 1: 0 });

// `Array.prototype.reduce` method
// https://tc39.github.io/ecma262/#sec-array.prototype.reduce
$({ target: 'Array', proto: true, forced: !STRICT_METHOD || !USES_TO_LENGTH }, {
  reduce: function reduce(callbackfn /* , initialValue */) {
    return $reduce(this, callbackfn, arguments.length, arguments.length > 1 ? arguments[1] : undefined);
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.array.slice.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.array.slice.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var isObject = __webpack_require__(/*! ../internals/is-object */ "./node_modules/core-js-pure/internals/is-object.js");
var isArray = __webpack_require__(/*! ../internals/is-array */ "./node_modules/core-js-pure/internals/is-array.js");
var toAbsoluteIndex = __webpack_require__(/*! ../internals/to-absolute-index */ "./node_modules/core-js-pure/internals/to-absolute-index.js");
var toLength = __webpack_require__(/*! ../internals/to-length */ "./node_modules/core-js-pure/internals/to-length.js");
var toIndexedObject = __webpack_require__(/*! ../internals/to-indexed-object */ "./node_modules/core-js-pure/internals/to-indexed-object.js");
var createProperty = __webpack_require__(/*! ../internals/create-property */ "./node_modules/core-js-pure/internals/create-property.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");
var arrayMethodHasSpeciesSupport = __webpack_require__(/*! ../internals/array-method-has-species-support */ "./node_modules/core-js-pure/internals/array-method-has-species-support.js");
var arrayMethodUsesToLength = __webpack_require__(/*! ../internals/array-method-uses-to-length */ "./node_modules/core-js-pure/internals/array-method-uses-to-length.js");

var HAS_SPECIES_SUPPORT = arrayMethodHasSpeciesSupport('slice');
var USES_TO_LENGTH = arrayMethodUsesToLength('slice', { ACCESSORS: true, 0: 0, 1: 2 });

var SPECIES = wellKnownSymbol('species');
var nativeSlice = [].slice;
var max = Math.max;

// `Array.prototype.slice` method
// https://tc39.github.io/ecma262/#sec-array.prototype.slice
// fallback for not array-like ES3 strings and DOM objects
$({ target: 'Array', proto: true, forced: !HAS_SPECIES_SUPPORT || !USES_TO_LENGTH }, {
  slice: function slice(start, end) {
    var O = toIndexedObject(this);
    var length = toLength(O.length);
    var k = toAbsoluteIndex(start, length);
    var fin = toAbsoluteIndex(end === undefined ? length : end, length);
    // inline `ArraySpeciesCreate` for usage native `Array#slice` where it's possible
    var Constructor, result, n;
    if (isArray(O)) {
      Constructor = O.constructor;
      // cross-realm fallback
      if (typeof Constructor == 'function' && (Constructor === Array || isArray(Constructor.prototype))) {
        Constructor = undefined;
      } else if (isObject(Constructor)) {
        Constructor = Constructor[SPECIES];
        if (Constructor === null) Constructor = undefined;
      }
      if (Constructor === Array || Constructor === undefined) {
        return nativeSlice.call(O, k, fin);
      }
    }
    result = new (Constructor === undefined ? Array : Constructor)(max(fin - k, 0));
    for (n = 0; k < fin; k++, n++) if (k in O) createProperty(result, n, O[k]);
    result.length = n;
    return result;
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.object.assign.js":
/*!***************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.object.assign.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var assign = __webpack_require__(/*! ../internals/object-assign */ "./node_modules/core-js-pure/internals/object-assign.js");

// `Object.assign` method
// https://tc39.github.io/ecma262/#sec-object.assign
$({ target: 'Object', stat: true, forced: Object.assign !== assign }, {
  assign: assign
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.object.define-properties.js":
/*!**************************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.object.define-properties.js ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var DESCRIPTORS = __webpack_require__(/*! ../internals/descriptors */ "./node_modules/core-js-pure/internals/descriptors.js");
var defineProperties = __webpack_require__(/*! ../internals/object-define-properties */ "./node_modules/core-js-pure/internals/object-define-properties.js");

// `Object.defineProperties` method
// https://tc39.github.io/ecma262/#sec-object.defineproperties
$({ target: 'Object', stat: true, forced: !DESCRIPTORS, sham: !DESCRIPTORS }, {
  defineProperties: defineProperties
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.object.get-own-property-descriptor.js":
/*!************************************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.object.get-own-property-descriptor.js ***!
  \************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var fails = __webpack_require__(/*! ../internals/fails */ "./node_modules/core-js-pure/internals/fails.js");
var toIndexedObject = __webpack_require__(/*! ../internals/to-indexed-object */ "./node_modules/core-js-pure/internals/to-indexed-object.js");
var nativeGetOwnPropertyDescriptor = __webpack_require__(/*! ../internals/object-get-own-property-descriptor */ "./node_modules/core-js-pure/internals/object-get-own-property-descriptor.js").f;
var DESCRIPTORS = __webpack_require__(/*! ../internals/descriptors */ "./node_modules/core-js-pure/internals/descriptors.js");

var FAILS_ON_PRIMITIVES = fails(function () { nativeGetOwnPropertyDescriptor(1); });
var FORCED = !DESCRIPTORS || FAILS_ON_PRIMITIVES;

// `Object.getOwnPropertyDescriptor` method
// https://tc39.github.io/ecma262/#sec-object.getownpropertydescriptor
$({ target: 'Object', stat: true, forced: FORCED, sham: !DESCRIPTORS }, {
  getOwnPropertyDescriptor: function getOwnPropertyDescriptor(it, key) {
    return nativeGetOwnPropertyDescriptor(toIndexedObject(it), key);
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.object.get-own-property-descriptors.js":
/*!*************************************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.object.get-own-property-descriptors.js ***!
  \*************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var DESCRIPTORS = __webpack_require__(/*! ../internals/descriptors */ "./node_modules/core-js-pure/internals/descriptors.js");
var ownKeys = __webpack_require__(/*! ../internals/own-keys */ "./node_modules/core-js-pure/internals/own-keys.js");
var toIndexedObject = __webpack_require__(/*! ../internals/to-indexed-object */ "./node_modules/core-js-pure/internals/to-indexed-object.js");
var getOwnPropertyDescriptorModule = __webpack_require__(/*! ../internals/object-get-own-property-descriptor */ "./node_modules/core-js-pure/internals/object-get-own-property-descriptor.js");
var createProperty = __webpack_require__(/*! ../internals/create-property */ "./node_modules/core-js-pure/internals/create-property.js");

// `Object.getOwnPropertyDescriptors` method
// https://tc39.github.io/ecma262/#sec-object.getownpropertydescriptors
$({ target: 'Object', stat: true, sham: !DESCRIPTORS }, {
  getOwnPropertyDescriptors: function getOwnPropertyDescriptors(object) {
    var O = toIndexedObject(object);
    var getOwnPropertyDescriptor = getOwnPropertyDescriptorModule.f;
    var keys = ownKeys(O);
    var result = {};
    var index = 0;
    var key, descriptor;
    while (keys.length > index) {
      descriptor = getOwnPropertyDescriptor(O, key = keys[index++]);
      if (descriptor !== undefined) createProperty(result, key, descriptor);
    }
    return result;
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.object.keys.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.object.keys.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var toObject = __webpack_require__(/*! ../internals/to-object */ "./node_modules/core-js-pure/internals/to-object.js");
var nativeKeys = __webpack_require__(/*! ../internals/object-keys */ "./node_modules/core-js-pure/internals/object-keys.js");
var fails = __webpack_require__(/*! ../internals/fails */ "./node_modules/core-js-pure/internals/fails.js");

var FAILS_ON_PRIMITIVES = fails(function () { nativeKeys(1); });

// `Object.keys` method
// https://tc39.github.io/ecma262/#sec-object.keys
$({ target: 'Object', stat: true, forced: FAILS_ON_PRIMITIVES }, {
  keys: function keys(it) {
    return nativeKeys(toObject(it));
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.promise.all-settled.js":
/*!*********************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.promise.all-settled.js ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var aFunction = __webpack_require__(/*! ../internals/a-function */ "./node_modules/core-js-pure/internals/a-function.js");
var newPromiseCapabilityModule = __webpack_require__(/*! ../internals/new-promise-capability */ "./node_modules/core-js-pure/internals/new-promise-capability.js");
var perform = __webpack_require__(/*! ../internals/perform */ "./node_modules/core-js-pure/internals/perform.js");
var iterate = __webpack_require__(/*! ../internals/iterate */ "./node_modules/core-js-pure/internals/iterate.js");

// `Promise.allSettled` method
// https://github.com/tc39/proposal-promise-allSettled
$({ target: 'Promise', stat: true }, {
  allSettled: function allSettled(iterable) {
    var C = this;
    var capability = newPromiseCapabilityModule.f(C);
    var resolve = capability.resolve;
    var reject = capability.reject;
    var result = perform(function () {
      var promiseResolve = aFunction(C.resolve);
      var values = [];
      var counter = 0;
      var remaining = 1;
      iterate(iterable, function (promise) {
        var index = counter++;
        var alreadyCalled = false;
        values.push(undefined);
        remaining++;
        promiseResolve.call(C, promise).then(function (value) {
          if (alreadyCalled) return;
          alreadyCalled = true;
          values[index] = { status: 'fulfilled', value: value };
          --remaining || resolve(values);
        }, function (e) {
          if (alreadyCalled) return;
          alreadyCalled = true;
          values[index] = { status: 'rejected', reason: e };
          --remaining || resolve(values);
        });
      });
      --remaining || resolve(values);
    });
    if (result.error) reject(result.value);
    return capability.promise;
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.promise.finally.js":
/*!*****************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.promise.finally.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var IS_PURE = __webpack_require__(/*! ../internals/is-pure */ "./node_modules/core-js-pure/internals/is-pure.js");
var NativePromise = __webpack_require__(/*! ../internals/native-promise-constructor */ "./node_modules/core-js-pure/internals/native-promise-constructor.js");
var fails = __webpack_require__(/*! ../internals/fails */ "./node_modules/core-js-pure/internals/fails.js");
var getBuiltIn = __webpack_require__(/*! ../internals/get-built-in */ "./node_modules/core-js-pure/internals/get-built-in.js");
var speciesConstructor = __webpack_require__(/*! ../internals/species-constructor */ "./node_modules/core-js-pure/internals/species-constructor.js");
var promiseResolve = __webpack_require__(/*! ../internals/promise-resolve */ "./node_modules/core-js-pure/internals/promise-resolve.js");
var redefine = __webpack_require__(/*! ../internals/redefine */ "./node_modules/core-js-pure/internals/redefine.js");

// Safari bug https://bugs.webkit.org/show_bug.cgi?id=200829
var NON_GENERIC = !!NativePromise && fails(function () {
  NativePromise.prototype['finally'].call({ then: function () { /* empty */ } }, function () { /* empty */ });
});

// `Promise.prototype.finally` method
// https://tc39.github.io/ecma262/#sec-promise.prototype.finally
$({ target: 'Promise', proto: true, real: true, forced: NON_GENERIC }, {
  'finally': function (onFinally) {
    var C = speciesConstructor(this, getBuiltIn('Promise'));
    var isFunction = typeof onFinally == 'function';
    return this.then(
      isFunction ? function (x) {
        return promiseResolve(C, onFinally()).then(function () { return x; });
      } : onFinally,
      isFunction ? function (e) {
        return promiseResolve(C, onFinally()).then(function () { throw e; });
      } : onFinally
    );
  }
});

// patch native Promise.prototype for native async functions
if (!IS_PURE && typeof NativePromise == 'function' && !NativePromise.prototype['finally']) {
  redefine(NativePromise.prototype, 'finally', getBuiltIn('Promise').prototype['finally']);
}


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.promise.js":
/*!*********************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.promise.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var IS_PURE = __webpack_require__(/*! ../internals/is-pure */ "./node_modules/core-js-pure/internals/is-pure.js");
var global = __webpack_require__(/*! ../internals/global */ "./node_modules/core-js-pure/internals/global.js");
var getBuiltIn = __webpack_require__(/*! ../internals/get-built-in */ "./node_modules/core-js-pure/internals/get-built-in.js");
var NativePromise = __webpack_require__(/*! ../internals/native-promise-constructor */ "./node_modules/core-js-pure/internals/native-promise-constructor.js");
var redefine = __webpack_require__(/*! ../internals/redefine */ "./node_modules/core-js-pure/internals/redefine.js");
var redefineAll = __webpack_require__(/*! ../internals/redefine-all */ "./node_modules/core-js-pure/internals/redefine-all.js");
var setToStringTag = __webpack_require__(/*! ../internals/set-to-string-tag */ "./node_modules/core-js-pure/internals/set-to-string-tag.js");
var setSpecies = __webpack_require__(/*! ../internals/set-species */ "./node_modules/core-js-pure/internals/set-species.js");
var isObject = __webpack_require__(/*! ../internals/is-object */ "./node_modules/core-js-pure/internals/is-object.js");
var aFunction = __webpack_require__(/*! ../internals/a-function */ "./node_modules/core-js-pure/internals/a-function.js");
var anInstance = __webpack_require__(/*! ../internals/an-instance */ "./node_modules/core-js-pure/internals/an-instance.js");
var classof = __webpack_require__(/*! ../internals/classof-raw */ "./node_modules/core-js-pure/internals/classof-raw.js");
var inspectSource = __webpack_require__(/*! ../internals/inspect-source */ "./node_modules/core-js-pure/internals/inspect-source.js");
var iterate = __webpack_require__(/*! ../internals/iterate */ "./node_modules/core-js-pure/internals/iterate.js");
var checkCorrectnessOfIteration = __webpack_require__(/*! ../internals/check-correctness-of-iteration */ "./node_modules/core-js-pure/internals/check-correctness-of-iteration.js");
var speciesConstructor = __webpack_require__(/*! ../internals/species-constructor */ "./node_modules/core-js-pure/internals/species-constructor.js");
var task = __webpack_require__(/*! ../internals/task */ "./node_modules/core-js-pure/internals/task.js").set;
var microtask = __webpack_require__(/*! ../internals/microtask */ "./node_modules/core-js-pure/internals/microtask.js");
var promiseResolve = __webpack_require__(/*! ../internals/promise-resolve */ "./node_modules/core-js-pure/internals/promise-resolve.js");
var hostReportErrors = __webpack_require__(/*! ../internals/host-report-errors */ "./node_modules/core-js-pure/internals/host-report-errors.js");
var newPromiseCapabilityModule = __webpack_require__(/*! ../internals/new-promise-capability */ "./node_modules/core-js-pure/internals/new-promise-capability.js");
var perform = __webpack_require__(/*! ../internals/perform */ "./node_modules/core-js-pure/internals/perform.js");
var InternalStateModule = __webpack_require__(/*! ../internals/internal-state */ "./node_modules/core-js-pure/internals/internal-state.js");
var isForced = __webpack_require__(/*! ../internals/is-forced */ "./node_modules/core-js-pure/internals/is-forced.js");
var wellKnownSymbol = __webpack_require__(/*! ../internals/well-known-symbol */ "./node_modules/core-js-pure/internals/well-known-symbol.js");
var V8_VERSION = __webpack_require__(/*! ../internals/engine-v8-version */ "./node_modules/core-js-pure/internals/engine-v8-version.js");

var SPECIES = wellKnownSymbol('species');
var PROMISE = 'Promise';
var getInternalState = InternalStateModule.get;
var setInternalState = InternalStateModule.set;
var getInternalPromiseState = InternalStateModule.getterFor(PROMISE);
var PromiseConstructor = NativePromise;
var TypeError = global.TypeError;
var document = global.document;
var process = global.process;
var $fetch = getBuiltIn('fetch');
var newPromiseCapability = newPromiseCapabilityModule.f;
var newGenericPromiseCapability = newPromiseCapability;
var IS_NODE = classof(process) == 'process';
var DISPATCH_EVENT = !!(document && document.createEvent && global.dispatchEvent);
var UNHANDLED_REJECTION = 'unhandledrejection';
var REJECTION_HANDLED = 'rejectionhandled';
var PENDING = 0;
var FULFILLED = 1;
var REJECTED = 2;
var HANDLED = 1;
var UNHANDLED = 2;
var Internal, OwnPromiseCapability, PromiseWrapper, nativeThen;

var FORCED = isForced(PROMISE, function () {
  var GLOBAL_CORE_JS_PROMISE = inspectSource(PromiseConstructor) !== String(PromiseConstructor);
  if (!GLOBAL_CORE_JS_PROMISE) {
    // V8 6.6 (Node 10 and Chrome 66) have a bug with resolving custom thenables
    // https://bugs.chromium.org/p/chromium/issues/detail?id=830565
    // We can't detect it synchronously, so just check versions
    if (V8_VERSION === 66) return true;
    // Unhandled rejections tracking support, NodeJS Promise without it fails @@species test
    if (!IS_NODE && typeof PromiseRejectionEvent != 'function') return true;
  }
  // We need Promise#finally in the pure version for preventing prototype pollution
  if (IS_PURE && !PromiseConstructor.prototype['finally']) return true;
  // We can't use @@species feature detection in V8 since it causes
  // deoptimization and performance degradation
  // https://github.com/zloirock/core-js/issues/679
  if (V8_VERSION >= 51 && /native code/.test(PromiseConstructor)) return false;
  // Detect correctness of subclassing with @@species support
  var promise = PromiseConstructor.resolve(1);
  var FakePromise = function (exec) {
    exec(function () { /* empty */ }, function () { /* empty */ });
  };
  var constructor = promise.constructor = {};
  constructor[SPECIES] = FakePromise;
  return !(promise.then(function () { /* empty */ }) instanceof FakePromise);
});

var INCORRECT_ITERATION = FORCED || !checkCorrectnessOfIteration(function (iterable) {
  PromiseConstructor.all(iterable)['catch'](function () { /* empty */ });
});

// helpers
var isThenable = function (it) {
  var then;
  return isObject(it) && typeof (then = it.then) == 'function' ? then : false;
};

var notify = function (promise, state, isReject) {
  if (state.notified) return;
  state.notified = true;
  var chain = state.reactions;
  microtask(function () {
    var value = state.value;
    var ok = state.state == FULFILLED;
    var index = 0;
    // variable length - can't use forEach
    while (chain.length > index) {
      var reaction = chain[index++];
      var handler = ok ? reaction.ok : reaction.fail;
      var resolve = reaction.resolve;
      var reject = reaction.reject;
      var domain = reaction.domain;
      var result, then, exited;
      try {
        if (handler) {
          if (!ok) {
            if (state.rejection === UNHANDLED) onHandleUnhandled(promise, state);
            state.rejection = HANDLED;
          }
          if (handler === true) result = value;
          else {
            if (domain) domain.enter();
            result = handler(value); // can throw
            if (domain) {
              domain.exit();
              exited = true;
            }
          }
          if (result === reaction.promise) {
            reject(TypeError('Promise-chain cycle'));
          } else if (then = isThenable(result)) {
            then.call(result, resolve, reject);
          } else resolve(result);
        } else reject(value);
      } catch (error) {
        if (domain && !exited) domain.exit();
        reject(error);
      }
    }
    state.reactions = [];
    state.notified = false;
    if (isReject && !state.rejection) onUnhandled(promise, state);
  });
};

var dispatchEvent = function (name, promise, reason) {
  var event, handler;
  if (DISPATCH_EVENT) {
    event = document.createEvent('Event');
    event.promise = promise;
    event.reason = reason;
    event.initEvent(name, false, true);
    global.dispatchEvent(event);
  } else event = { promise: promise, reason: reason };
  if (handler = global['on' + name]) handler(event);
  else if (name === UNHANDLED_REJECTION) hostReportErrors('Unhandled promise rejection', reason);
};

var onUnhandled = function (promise, state) {
  task.call(global, function () {
    var value = state.value;
    var IS_UNHANDLED = isUnhandled(state);
    var result;
    if (IS_UNHANDLED) {
      result = perform(function () {
        if (IS_NODE) {
          process.emit('unhandledRejection', value, promise);
        } else dispatchEvent(UNHANDLED_REJECTION, promise, value);
      });
      // Browsers should not trigger `rejectionHandled` event if it was handled here, NodeJS - should
      state.rejection = IS_NODE || isUnhandled(state) ? UNHANDLED : HANDLED;
      if (result.error) throw result.value;
    }
  });
};

var isUnhandled = function (state) {
  return state.rejection !== HANDLED && !state.parent;
};

var onHandleUnhandled = function (promise, state) {
  task.call(global, function () {
    if (IS_NODE) {
      process.emit('rejectionHandled', promise);
    } else dispatchEvent(REJECTION_HANDLED, promise, state.value);
  });
};

var bind = function (fn, promise, state, unwrap) {
  return function (value) {
    fn(promise, state, value, unwrap);
  };
};

var internalReject = function (promise, state, value, unwrap) {
  if (state.done) return;
  state.done = true;
  if (unwrap) state = unwrap;
  state.value = value;
  state.state = REJECTED;
  notify(promise, state, true);
};

var internalResolve = function (promise, state, value, unwrap) {
  if (state.done) return;
  state.done = true;
  if (unwrap) state = unwrap;
  try {
    if (promise === value) throw TypeError("Promise can't be resolved itself");
    var then = isThenable(value);
    if (then) {
      microtask(function () {
        var wrapper = { done: false };
        try {
          then.call(value,
            bind(internalResolve, promise, wrapper, state),
            bind(internalReject, promise, wrapper, state)
          );
        } catch (error) {
          internalReject(promise, wrapper, error, state);
        }
      });
    } else {
      state.value = value;
      state.state = FULFILLED;
      notify(promise, state, false);
    }
  } catch (error) {
    internalReject(promise, { done: false }, error, state);
  }
};

// constructor polyfill
if (FORCED) {
  // 25.4.3.1 Promise(executor)
  PromiseConstructor = function Promise(executor) {
    anInstance(this, PromiseConstructor, PROMISE);
    aFunction(executor);
    Internal.call(this);
    var state = getInternalState(this);
    try {
      executor(bind(internalResolve, this, state), bind(internalReject, this, state));
    } catch (error) {
      internalReject(this, state, error);
    }
  };
  // eslint-disable-next-line no-unused-vars
  Internal = function Promise(executor) {
    setInternalState(this, {
      type: PROMISE,
      done: false,
      notified: false,
      parent: false,
      reactions: [],
      rejection: false,
      state: PENDING,
      value: undefined
    });
  };
  Internal.prototype = redefineAll(PromiseConstructor.prototype, {
    // `Promise.prototype.then` method
    // https://tc39.github.io/ecma262/#sec-promise.prototype.then
    then: function then(onFulfilled, onRejected) {
      var state = getInternalPromiseState(this);
      var reaction = newPromiseCapability(speciesConstructor(this, PromiseConstructor));
      reaction.ok = typeof onFulfilled == 'function' ? onFulfilled : true;
      reaction.fail = typeof onRejected == 'function' && onRejected;
      reaction.domain = IS_NODE ? process.domain : undefined;
      state.parent = true;
      state.reactions.push(reaction);
      if (state.state != PENDING) notify(this, state, false);
      return reaction.promise;
    },
    // `Promise.prototype.catch` method
    // https://tc39.github.io/ecma262/#sec-promise.prototype.catch
    'catch': function (onRejected) {
      return this.then(undefined, onRejected);
    }
  });
  OwnPromiseCapability = function () {
    var promise = new Internal();
    var state = getInternalState(promise);
    this.promise = promise;
    this.resolve = bind(internalResolve, promise, state);
    this.reject = bind(internalReject, promise, state);
  };
  newPromiseCapabilityModule.f = newPromiseCapability = function (C) {
    return C === PromiseConstructor || C === PromiseWrapper
      ? new OwnPromiseCapability(C)
      : newGenericPromiseCapability(C);
  };

  if (!IS_PURE && typeof NativePromise == 'function') {
    nativeThen = NativePromise.prototype.then;

    // wrap native Promise#then for native async functions
    redefine(NativePromise.prototype, 'then', function then(onFulfilled, onRejected) {
      var that = this;
      return new PromiseConstructor(function (resolve, reject) {
        nativeThen.call(that, resolve, reject);
      }).then(onFulfilled, onRejected);
    // https://github.com/zloirock/core-js/issues/640
    }, { unsafe: true });

    // wrap fetch result
    if (typeof $fetch == 'function') $({ global: true, enumerable: true, forced: true }, {
      // eslint-disable-next-line no-unused-vars
      fetch: function fetch(input /* , init */) {
        return promiseResolve(PromiseConstructor, $fetch.apply(global, arguments));
      }
    });
  }
}

$({ global: true, wrap: true, forced: FORCED }, {
  Promise: PromiseConstructor
});

setToStringTag(PromiseConstructor, PROMISE, false, true);
setSpecies(PROMISE);

PromiseWrapper = getBuiltIn(PROMISE);

// statics
$({ target: PROMISE, stat: true, forced: FORCED }, {
  // `Promise.reject` method
  // https://tc39.github.io/ecma262/#sec-promise.reject
  reject: function reject(r) {
    var capability = newPromiseCapability(this);
    capability.reject.call(undefined, r);
    return capability.promise;
  }
});

$({ target: PROMISE, stat: true, forced: IS_PURE || FORCED }, {
  // `Promise.resolve` method
  // https://tc39.github.io/ecma262/#sec-promise.resolve
  resolve: function resolve(x) {
    return promiseResolve(IS_PURE && this === PromiseWrapper ? PromiseConstructor : this, x);
  }
});

$({ target: PROMISE, stat: true, forced: INCORRECT_ITERATION }, {
  // `Promise.all` method
  // https://tc39.github.io/ecma262/#sec-promise.all
  all: function all(iterable) {
    var C = this;
    var capability = newPromiseCapability(C);
    var resolve = capability.resolve;
    var reject = capability.reject;
    var result = perform(function () {
      var $promiseResolve = aFunction(C.resolve);
      var values = [];
      var counter = 0;
      var remaining = 1;
      iterate(iterable, function (promise) {
        var index = counter++;
        var alreadyCalled = false;
        values.push(undefined);
        remaining++;
        $promiseResolve.call(C, promise).then(function (value) {
          if (alreadyCalled) return;
          alreadyCalled = true;
          values[index] = value;
          --remaining || resolve(values);
        }, reject);
      });
      --remaining || resolve(values);
    });
    if (result.error) reject(result.value);
    return capability.promise;
  },
  // `Promise.race` method
  // https://tc39.github.io/ecma262/#sec-promise.race
  race: function race(iterable) {
    var C = this;
    var capability = newPromiseCapability(C);
    var reject = capability.reject;
    var result = perform(function () {
      var $promiseResolve = aFunction(C.resolve);
      iterate(iterable, function (promise) {
        $promiseResolve.call(C, promise).then(capability.resolve, reject);
      });
    });
    if (result.error) reject(result.value);
    return capability.promise;
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.string.includes.js":
/*!*****************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.string.includes.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var notARegExp = __webpack_require__(/*! ../internals/not-a-regexp */ "./node_modules/core-js-pure/internals/not-a-regexp.js");
var requireObjectCoercible = __webpack_require__(/*! ../internals/require-object-coercible */ "./node_modules/core-js-pure/internals/require-object-coercible.js");
var correctIsRegExpLogic = __webpack_require__(/*! ../internals/correct-is-regexp-logic */ "./node_modules/core-js-pure/internals/correct-is-regexp-logic.js");

// `String.prototype.includes` method
// https://tc39.github.io/ecma262/#sec-string.prototype.includes
$({ target: 'String', proto: true, forced: !correctIsRegExpLogic('includes') }, {
  includes: function includes(searchString /* , position = 0 */) {
    return !!~String(requireObjectCoercible(this))
      .indexOf(notARegExp(searchString), arguments.length > 1 ? arguments[1] : undefined);
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/es.string.trim.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/es.string.trim.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var $trim = __webpack_require__(/*! ../internals/string-trim */ "./node_modules/core-js-pure/internals/string-trim.js").trim;
var forcedStringTrimMethod = __webpack_require__(/*! ../internals/string-trim-forced */ "./node_modules/core-js-pure/internals/string-trim-forced.js");

// `String.prototype.trim` method
// https://tc39.github.io/ecma262/#sec-string.prototype.trim
$({ target: 'String', proto: true, forced: forcedStringTrimMethod('trim') }, {
  trim: function trim() {
    return $trim(this);
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/esnext.aggregate-error.js":
/*!*********************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/esnext.aggregate-error.js ***!
  \*********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var DESCRIPTORS = __webpack_require__(/*! ../internals/descriptors */ "./node_modules/core-js-pure/internals/descriptors.js");
var getPrototypeOf = __webpack_require__(/*! ../internals/object-get-prototype-of */ "./node_modules/core-js-pure/internals/object-get-prototype-of.js");
var setPrototypeOf = __webpack_require__(/*! ../internals/object-set-prototype-of */ "./node_modules/core-js-pure/internals/object-set-prototype-of.js");
var create = __webpack_require__(/*! ../internals/object-create */ "./node_modules/core-js-pure/internals/object-create.js");
var defineProperty = __webpack_require__(/*! ../internals/object-define-property */ "./node_modules/core-js-pure/internals/object-define-property.js");
var createPropertyDescriptor = __webpack_require__(/*! ../internals/create-property-descriptor */ "./node_modules/core-js-pure/internals/create-property-descriptor.js");
var iterate = __webpack_require__(/*! ../internals/iterate */ "./node_modules/core-js-pure/internals/iterate.js");
var createNonEnumerableProperty = __webpack_require__(/*! ../internals/create-non-enumerable-property */ "./node_modules/core-js-pure/internals/create-non-enumerable-property.js");
var InternalStateModule = __webpack_require__(/*! ../internals/internal-state */ "./node_modules/core-js-pure/internals/internal-state.js");

var setInternalState = InternalStateModule.set;
var getInternalAggregateErrorState = InternalStateModule.getterFor('AggregateError');

var $AggregateError = function AggregateError(errors, message) {
  var that = this;
  if (!(that instanceof $AggregateError)) return new $AggregateError(errors, message);
  if (setPrototypeOf) {
    that = setPrototypeOf(new Error(message), getPrototypeOf(that));
  }
  var errorsArray = [];
  iterate(errors, errorsArray.push, errorsArray);
  if (DESCRIPTORS) setInternalState(that, { errors: errorsArray, type: 'AggregateError' });
  else that.errors = errorsArray;
  if (message !== undefined) createNonEnumerableProperty(that, 'message', String(message));
  return that;
};

$AggregateError.prototype = create(Error.prototype, {
  constructor: createPropertyDescriptor(5, $AggregateError),
  message: createPropertyDescriptor(5, ''),
  name: createPropertyDescriptor(5, 'AggregateError')
});

if (DESCRIPTORS) defineProperty.f($AggregateError.prototype, 'errors', {
  get: function () {
    return getInternalAggregateErrorState(this).errors;
  },
  configurable: true
});

$({ global: true }, {
  AggregateError: $AggregateError
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/esnext.promise.all-settled.js":
/*!*************************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/esnext.promise.all-settled.js ***!
  \*************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

// TODO: Remove from `core-js@4`
__webpack_require__(/*! ./es.promise.all-settled.js */ "./node_modules/core-js-pure/modules/es.promise.all-settled.js");


/***/ }),

/***/ "./node_modules/core-js-pure/modules/esnext.promise.any.js":
/*!*****************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/esnext.promise.any.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var aFunction = __webpack_require__(/*! ../internals/a-function */ "./node_modules/core-js-pure/internals/a-function.js");
var getBuiltIn = __webpack_require__(/*! ../internals/get-built-in */ "./node_modules/core-js-pure/internals/get-built-in.js");
var newPromiseCapabilityModule = __webpack_require__(/*! ../internals/new-promise-capability */ "./node_modules/core-js-pure/internals/new-promise-capability.js");
var perform = __webpack_require__(/*! ../internals/perform */ "./node_modules/core-js-pure/internals/perform.js");
var iterate = __webpack_require__(/*! ../internals/iterate */ "./node_modules/core-js-pure/internals/iterate.js");

var PROMISE_ANY_ERROR = 'No one promise resolved';

// `Promise.any` method
// https://github.com/tc39/proposal-promise-any
$({ target: 'Promise', stat: true }, {
  any: function any(iterable) {
    var C = this;
    var capability = newPromiseCapabilityModule.f(C);
    var resolve = capability.resolve;
    var reject = capability.reject;
    var result = perform(function () {
      var promiseResolve = aFunction(C.resolve);
      var errors = [];
      var counter = 0;
      var remaining = 1;
      var alreadyResolved = false;
      iterate(iterable, function (promise) {
        var index = counter++;
        var alreadyRejected = false;
        errors.push(undefined);
        remaining++;
        promiseResolve.call(C, promise).then(function (value) {
          if (alreadyRejected || alreadyResolved) return;
          alreadyResolved = true;
          resolve(value);
        }, function (e) {
          if (alreadyRejected || alreadyResolved) return;
          alreadyRejected = true;
          errors[index] = e;
          --remaining || reject(new (getBuiltIn('AggregateError'))(errors, PROMISE_ANY_ERROR));
        });
      });
      --remaining || reject(new (getBuiltIn('AggregateError'))(errors, PROMISE_ANY_ERROR));
    });
    if (result.error) reject(result.value);
    return capability.promise;
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/modules/esnext.promise.try.js":
/*!*****************************************************************!*\
  !*** ./node_modules/core-js-pure/modules/esnext.promise.try.js ***!
  \*****************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var $ = __webpack_require__(/*! ../internals/export */ "./node_modules/core-js-pure/internals/export.js");
var newPromiseCapabilityModule = __webpack_require__(/*! ../internals/new-promise-capability */ "./node_modules/core-js-pure/internals/new-promise-capability.js");
var perform = __webpack_require__(/*! ../internals/perform */ "./node_modules/core-js-pure/internals/perform.js");

// `Promise.try` method
// https://github.com/tc39/proposal-promise-try
$({ target: 'Promise', stat: true }, {
  'try': function (callbackfn) {
    var promiseCapability = newPromiseCapabilityModule.f(this);
    var result = perform(callbackfn);
    (result.error ? promiseCapability.reject : promiseCapability.resolve)(result.value);
    return promiseCapability.promise;
  }
});


/***/ }),

/***/ "./node_modules/core-js-pure/stable/array/from.js":
/*!********************************************************!*\
  !*** ./node_modules/core-js-pure/stable/array/from.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/array/from */ "./node_modules/core-js-pure/es/array/from.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/concat.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/concat.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/concat */ "./node_modules/core-js-pure/es/instance/concat.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/includes.js":
/*!***************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/includes.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/includes */ "./node_modules/core-js-pure/es/instance/includes.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/index-of.js":
/*!***************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/index-of.js ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/index-of */ "./node_modules/core-js-pure/es/instance/index-of.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/last-index-of.js":
/*!********************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/last-index-of.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/last-index-of */ "./node_modules/core-js-pure/es/instance/last-index-of.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/reduce.js":
/*!*************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/reduce.js ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/reduce */ "./node_modules/core-js-pure/es/instance/reduce.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/slice.js":
/*!************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/slice.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/slice */ "./node_modules/core-js-pure/es/instance/slice.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/instance/trim.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/stable/instance/trim.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/instance/trim */ "./node_modules/core-js-pure/es/instance/trim.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/assign.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/assign.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/assign */ "./node_modules/core-js-pure/es/object/assign.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/define-properties.js":
/*!**********************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/define-properties.js ***!
  \**********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/define-properties */ "./node_modules/core-js-pure/es/object/define-properties.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/define-property.js":
/*!********************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/define-property.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/define-property */ "./node_modules/core-js-pure/es/object/define-property.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/get-own-property-descriptor.js":
/*!********************************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/get-own-property-descriptor.js ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/get-own-property-descriptor */ "./node_modules/core-js-pure/es/object/get-own-property-descriptor.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/get-own-property-descriptors.js":
/*!*********************************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/get-own-property-descriptors.js ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/get-own-property-descriptors */ "./node_modules/core-js-pure/es/object/get-own-property-descriptors.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/get-own-property-symbols.js":
/*!*****************************************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/get-own-property-symbols.js ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/get-own-property-symbols */ "./node_modules/core-js-pure/es/object/get-own-property-symbols.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/object/keys.js":
/*!*********************************************************!*\
  !*** ./node_modules/core-js-pure/stable/object/keys.js ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/object/keys */ "./node_modules/core-js-pure/es/object/keys.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/core-js-pure/stable/promise/index.js":
/*!***********************************************************!*\
  !*** ./node_modules/core-js-pure/stable/promise/index.js ***!
  \***********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

var parent = __webpack_require__(/*! ../../es/promise */ "./node_modules/core-js-pure/es/promise/index.js");

module.exports = parent;


/***/ }),

/***/ "./node_modules/is-buffer/index.js":
/*!*****************************************!*\
  !*** ./node_modules/is-buffer/index.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

/*!
 * Determine if an object is a Buffer
 *
 * @author   Feross Aboukhadijeh <https://feross.org>
 * @license  MIT
 */

module.exports = function isBuffer (obj) {
  return obj != null && obj.constructor != null &&
    typeof obj.constructor.isBuffer === 'function' && obj.constructor.isBuffer(obj)
}


/***/ }),

/***/ "./node_modules/process/browser.js":
/*!*****************************************!*\
  !*** ./node_modules/process/browser.js ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

// shim for using process in browser
var process = module.exports = {};

// cached from whatever global is present so that test runners that stub it
// don't break things.  But we need to wrap it in a try catch in case it is
// wrapped in strict mode code which doesn't define any globals.  It's inside a
// function because try/catches deoptimize in certain engines.

var cachedSetTimeout;
var cachedClearTimeout;

function defaultSetTimout() {
    throw new Error('setTimeout has not been defined');
}
function defaultClearTimeout () {
    throw new Error('clearTimeout has not been defined');
}
(function () {
    try {
        if (typeof setTimeout === 'function') {
            cachedSetTimeout = setTimeout;
        } else {
            cachedSetTimeout = defaultSetTimout;
        }
    } catch (e) {
        cachedSetTimeout = defaultSetTimout;
    }
    try {
        if (typeof clearTimeout === 'function') {
            cachedClearTimeout = clearTimeout;
        } else {
            cachedClearTimeout = defaultClearTimeout;
        }
    } catch (e) {
        cachedClearTimeout = defaultClearTimeout;
    }
} ())
function runTimeout(fun) {
    if (cachedSetTimeout === setTimeout) {
        //normal enviroments in sane situations
        return setTimeout(fun, 0);
    }
    // if setTimeout wasn't available but was latter defined
    if ((cachedSetTimeout === defaultSetTimout || !cachedSetTimeout) && setTimeout) {
        cachedSetTimeout = setTimeout;
        return setTimeout(fun, 0);
    }
    try {
        // when when somebody has screwed with setTimeout but no I.E. maddness
        return cachedSetTimeout(fun, 0);
    } catch(e){
        try {
            // When we are in I.E. but the script has been evaled so I.E. doesn't trust the global object when called normally
            return cachedSetTimeout.call(null, fun, 0);
        } catch(e){
            // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error
            return cachedSetTimeout.call(this, fun, 0);
        }
    }


}
function runClearTimeout(marker) {
    if (cachedClearTimeout === clearTimeout) {
        //normal enviroments in sane situations
        return clearTimeout(marker);
    }
    // if clearTimeout wasn't available but was latter defined
    if ((cachedClearTimeout === defaultClearTimeout || !cachedClearTimeout) && clearTimeout) {
        cachedClearTimeout = clearTimeout;
        return clearTimeout(marker);
    }
    try {
        // when when somebody has screwed with setTimeout but no I.E. maddness
        return cachedClearTimeout(marker);
    } catch (e){
        try {
            // When we are in I.E. but the script has been evaled so I.E. doesn't  trust the global object when called normally
            return cachedClearTimeout.call(null, marker);
        } catch (e){
            // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error.
            // Some versions of I.E. have different rules for clearTimeout vs setTimeout
            return cachedClearTimeout.call(this, marker);
        }
    }



}
var queue = [];
var draining = false;
var currentQueue;
var queueIndex = -1;

function cleanUpNextTick() {
    if (!draining || !currentQueue) {
        return;
    }
    draining = false;
    if (currentQueue.length) {
        queue = currentQueue.concat(queue);
    } else {
        queueIndex = -1;
    }
    if (queue.length) {
        drainQueue();
    }
}

function drainQueue() {
    if (draining) {
        return;
    }
    var timeout = runTimeout(cleanUpNextTick);
    draining = true;

    var len = queue.length;
    while(len) {
        currentQueue = queue;
        queue = [];
        while (++queueIndex < len) {
            if (currentQueue) {
                currentQueue[queueIndex].run();
            }
        }
        queueIndex = -1;
        len = queue.length;
    }
    currentQueue = null;
    draining = false;
    runClearTimeout(timeout);
}

process.nextTick = function (fun) {
    var args = new Array(arguments.length - 1);
    if (arguments.length > 1) {
        for (var i = 1; i < arguments.length; i++) {
            args[i - 1] = arguments[i];
        }
    }
    queue.push(new Item(fun, args));
    if (queue.length === 1 && !draining) {
        runTimeout(drainQueue);
    }
};

// v8 likes predictible objects
function Item(fun, array) {
    this.fun = fun;
    this.array = array;
}
Item.prototype.run = function () {
    this.fun.apply(null, this.array);
};
process.title = 'browser';
process.browser = true;
process.env = {};
process.argv = [];
process.version = ''; // empty string to avoid regexp issues
process.versions = {};

function noop() {}

process.on = noop;
process.addListener = noop;
process.once = noop;
process.off = noop;
process.removeListener = noop;
process.removeAllListeners = noop;
process.emit = noop;
process.prependListener = noop;
process.prependOnceListener = noop;

process.listeners = function (name) { return [] }

process.binding = function (name) {
    throw new Error('process.binding is not supported');
};

process.cwd = function () { return '/' };
process.chdir = function (dir) {
    throw new Error('process.chdir is not supported');
};
process.umask = function() { return 0; };


/***/ }),

/***/ "./node_modules/react-redux/es/index.js":
/*!*************************************************************************************************************!*\
  !*** delegated ../../../node_modules/react-redux/es/index.js from dll-reference react_23efd8d7d6577d253464 ***!
  \*************************************************************************************************************/
/*! exports provided: Provider, connectAdvanced, ReactReduxContext, connect, batch, useDispatch, createDispatchHook, useSelector, createSelectorHook, useStore, createStoreHook, shallowEqual */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_23efd8d7d6577d253464 */ "dll-reference react_23efd8d7d6577d253464"))(166);

/***/ }),

/***/ "./node_modules/reactstrap/es/index.js":
/*!************************************************************************************************************!*\
  !*** delegated ../../../node_modules/reactstrap/es/index.js from dll-reference react_23efd8d7d6577d253464 ***!
  \************************************************************************************************************/
/*! exports provided: Container, Row, Col, Navbar, NavbarBrand, NavbarText, NavbarToggler, Nav, NavItem, NavLink, Breadcrumb, BreadcrumbItem, Button, ButtonToggle, ButtonDropdown, ButtonGroup, ButtonToolbar, Dropdown, DropdownItem, DropdownMenu, DropdownToggle, DropdownContext, Fade, Badge, Card, CardGroup, CardDeck, CardColumns, CardBody, CardLink, CardFooter, CardHeader, CardImg, CardImgOverlay, Carousel, UncontrolledCarousel, CarouselControl, CarouselItem, CarouselIndicators, CarouselCaption, CardSubtitle, CardText, CardTitle, CustomFileInput, CustomInput, PopperContent, PopperTargetHelper, Popover, UncontrolledPopover, PopoverHeader, PopoverBody, Progress, Modal, ModalHeader, ModalBody, ModalFooter, Tooltip, Table, ListGroup, Form, FormFeedback, FormGroup, FormText, Input, InputGroup, InputGroupAddon, InputGroupButtonDropdown, InputGroupText, Label, Media, Pagination, PaginationItem, PaginationLink, TabContent, TabPane, Jumbotron, Alert, Toast, ToastBody, ToastHeader, Collapse, ListGroupItem, ListGroupItemHeading, ListGroupItemText, UncontrolledAlert, UncontrolledButtonDropdown, UncontrolledCollapse, UncontrolledDropdown, UncontrolledTooltip, Spinner, Util, Polyfill */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_23efd8d7d6577d253464 */ "dll-reference react_23efd8d7d6577d253464"))(173);

/***/ }),

/***/ "./node_modules/redux-logger/dist/redux-logger.js":
/*!********************************************************!*\
  !*** ./node_modules/redux-logger/dist/redux-logger.js ***!
  \********************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

/* WEBPACK VAR INJECTION */(function(global) {!function(e,t){ true?t(exports):undefined}(this,function(e){"use strict";function t(e,t){e.super_=t,e.prototype=Object.create(t.prototype,{constructor:{value:e,enumerable:!1,writable:!0,configurable:!0}})}function r(e,t){Object.defineProperty(this,"kind",{value:e,enumerable:!0}),t&&t.length&&Object.defineProperty(this,"path",{value:t,enumerable:!0})}function n(e,t,r){n.super_.call(this,"E",e),Object.defineProperty(this,"lhs",{value:t,enumerable:!0}),Object.defineProperty(this,"rhs",{value:r,enumerable:!0})}function o(e,t){o.super_.call(this,"N",e),Object.defineProperty(this,"rhs",{value:t,enumerable:!0})}function i(e,t){i.super_.call(this,"D",e),Object.defineProperty(this,"lhs",{value:t,enumerable:!0})}function a(e,t,r){a.super_.call(this,"A",e),Object.defineProperty(this,"index",{value:t,enumerable:!0}),Object.defineProperty(this,"item",{value:r,enumerable:!0})}function f(e,t,r){var n=e.slice((r||t)+1||e.length);return e.length=t<0?e.length+t:t,e.push.apply(e,n),e}function u(e){var t="undefined"==typeof e?"undefined":N(e);return"object"!==t?t:e===Math?"math":null===e?"null":Array.isArray(e)?"array":"[object Date]"===Object.prototype.toString.call(e)?"date":"function"==typeof e.toString&&/^\/.*\//.test(e.toString())?"regexp":"object"}function l(e,t,r,c,s,d,p){s=s||[],p=p||[];var g=s.slice(0);if("undefined"!=typeof d){if(c){if("function"==typeof c&&c(g,d))return;if("object"===("undefined"==typeof c?"undefined":N(c))){if(c.prefilter&&c.prefilter(g,d))return;if(c.normalize){var h=c.normalize(g,d,e,t);h&&(e=h[0],t=h[1])}}}g.push(d)}"regexp"===u(e)&&"regexp"===u(t)&&(e=e.toString(),t=t.toString());var y="undefined"==typeof e?"undefined":N(e),v="undefined"==typeof t?"undefined":N(t),b="undefined"!==y||p&&p[p.length-1].lhs&&p[p.length-1].lhs.hasOwnProperty(d),m="undefined"!==v||p&&p[p.length-1].rhs&&p[p.length-1].rhs.hasOwnProperty(d);if(!b&&m)r(new o(g,t));else if(!m&&b)r(new i(g,e));else if(u(e)!==u(t))r(new n(g,e,t));else if("date"===u(e)&&e-t!==0)r(new n(g,e,t));else if("object"===y&&null!==e&&null!==t)if(p.filter(function(t){return t.lhs===e}).length)e!==t&&r(new n(g,e,t));else{if(p.push({lhs:e,rhs:t}),Array.isArray(e)){var w;e.length;for(w=0;w<e.length;w++)w>=t.length?r(new a(g,w,new i(void 0,e[w]))):l(e[w],t[w],r,c,g,w,p);for(;w<t.length;)r(new a(g,w,new o(void 0,t[w++])))}else{var x=Object.keys(e),S=Object.keys(t);x.forEach(function(n,o){var i=S.indexOf(n);i>=0?(l(e[n],t[n],r,c,g,n,p),S=f(S,i)):l(e[n],void 0,r,c,g,n,p)}),S.forEach(function(e){l(void 0,t[e],r,c,g,e,p)})}p.length=p.length-1}else e!==t&&("number"===y&&isNaN(e)&&isNaN(t)||r(new n(g,e,t)))}function c(e,t,r,n){return n=n||[],l(e,t,function(e){e&&n.push(e)},r),n.length?n:void 0}function s(e,t,r){if(r.path&&r.path.length){var n,o=e[t],i=r.path.length-1;for(n=0;n<i;n++)o=o[r.path[n]];switch(r.kind){case"A":s(o[r.path[n]],r.index,r.item);break;case"D":delete o[r.path[n]];break;case"E":case"N":o[r.path[n]]=r.rhs}}else switch(r.kind){case"A":s(e[t],r.index,r.item);break;case"D":e=f(e,t);break;case"E":case"N":e[t]=r.rhs}return e}function d(e,t,r){if(e&&t&&r&&r.kind){for(var n=e,o=-1,i=r.path?r.path.length-1:0;++o<i;)"undefined"==typeof n[r.path[o]]&&(n[r.path[o]]="number"==typeof r.path[o]?[]:{}),n=n[r.path[o]];switch(r.kind){case"A":s(r.path?n[r.path[o]]:n,r.index,r.item);break;case"D":delete n[r.path[o]];break;case"E":case"N":n[r.path[o]]=r.rhs}}}function p(e,t,r){if(r.path&&r.path.length){var n,o=e[t],i=r.path.length-1;for(n=0;n<i;n++)o=o[r.path[n]];switch(r.kind){case"A":p(o[r.path[n]],r.index,r.item);break;case"D":o[r.path[n]]=r.lhs;break;case"E":o[r.path[n]]=r.lhs;break;case"N":delete o[r.path[n]]}}else switch(r.kind){case"A":p(e[t],r.index,r.item);break;case"D":e[t]=r.lhs;break;case"E":e[t]=r.lhs;break;case"N":e=f(e,t)}return e}function g(e,t,r){if(e&&t&&r&&r.kind){var n,o,i=e;for(o=r.path.length-1,n=0;n<o;n++)"undefined"==typeof i[r.path[n]]&&(i[r.path[n]]={}),i=i[r.path[n]];switch(r.kind){case"A":p(i[r.path[n]],r.index,r.item);break;case"D":i[r.path[n]]=r.lhs;break;case"E":i[r.path[n]]=r.lhs;break;case"N":delete i[r.path[n]]}}}function h(e,t,r){if(e&&t){var n=function(n){r&&!r(e,t,n)||d(e,t,n)};l(e,t,n)}}function y(e){return"color: "+F[e].color+"; font-weight: bold"}function v(e){var t=e.kind,r=e.path,n=e.lhs,o=e.rhs,i=e.index,a=e.item;switch(t){case"E":return[r.join("."),n,"→",o];case"N":return[r.join("."),o];case"D":return[r.join(".")];case"A":return[r.join(".")+"["+i+"]",a];default:return[]}}function b(e,t,r,n){var o=c(e,t);try{n?r.groupCollapsed("diff"):r.group("diff")}catch(e){r.log("diff")}o?o.forEach(function(e){var t=e.kind,n=v(e);r.log.apply(r,["%c "+F[t].text,y(t)].concat(P(n)))}):r.log("—— no diff ——");try{r.groupEnd()}catch(e){r.log("—— diff end —— ")}}function m(e,t,r,n){switch("undefined"==typeof e?"undefined":N(e)){case"object":return"function"==typeof e[n]?e[n].apply(e,P(r)):e[n];case"function":return e(t);default:return e}}function w(e){var t=e.timestamp,r=e.duration;return function(e,n,o){var i=["action"];return i.push("%c"+String(e.type)),t&&i.push("%c@ "+n),r&&i.push("%c(in "+o.toFixed(2)+" ms)"),i.join(" ")}}function x(e,t){var r=t.logger,n=t.actionTransformer,o=t.titleFormatter,i=void 0===o?w(t):o,a=t.collapsed,f=t.colors,u=t.level,l=t.diff,c="undefined"==typeof t.titleFormatter;e.forEach(function(o,s){var d=o.started,p=o.startedTime,g=o.action,h=o.prevState,y=o.error,v=o.took,w=o.nextState,x=e[s+1];x&&(w=x.prevState,v=x.started-d);var S=n(g),k="function"==typeof a?a(function(){return w},g,o):a,j=D(p),E=f.title?"color: "+f.title(S)+";":"",A=["color: gray; font-weight: lighter;"];A.push(E),t.timestamp&&A.push("color: gray; font-weight: lighter;"),t.duration&&A.push("color: gray; font-weight: lighter;");var O=i(S,j,v);try{k?f.title&&c?r.groupCollapsed.apply(r,["%c "+O].concat(A)):r.groupCollapsed(O):f.title&&c?r.group.apply(r,["%c "+O].concat(A)):r.group(O)}catch(e){r.log(O)}var N=m(u,S,[h],"prevState"),P=m(u,S,[S],"action"),C=m(u,S,[y,h],"error"),F=m(u,S,[w],"nextState");if(N)if(f.prevState){var L="color: "+f.prevState(h)+"; font-weight: bold";r[N]("%c prev state",L,h)}else r[N]("prev state",h);if(P)if(f.action){var T="color: "+f.action(S)+"; font-weight: bold";r[P]("%c action    ",T,S)}else r[P]("action    ",S);if(y&&C)if(f.error){var M="color: "+f.error(y,h)+"; font-weight: bold;";r[C]("%c error     ",M,y)}else r[C]("error     ",y);if(F)if(f.nextState){var _="color: "+f.nextState(w)+"; font-weight: bold";r[F]("%c next state",_,w)}else r[F]("next state",w);l&&b(h,w,r,k);try{r.groupEnd()}catch(e){r.log("—— log end ——")}})}function S(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},t=Object.assign({},L,e),r=t.logger,n=t.stateTransformer,o=t.errorTransformer,i=t.predicate,a=t.logErrors,f=t.diffPredicate;if("undefined"==typeof r)return function(){return function(e){return function(t){return e(t)}}};if(e.getState&&e.dispatch)return console.error("[redux-logger] redux-logger not installed. Make sure to pass logger instance as middleware:\n// Logger with default options\nimport { logger } from 'redux-logger'\nconst store = createStore(\n  reducer,\n  applyMiddleware(logger)\n)\n// Or you can create your own logger with custom options http://bit.ly/redux-logger-options\nimport createLogger from 'redux-logger'\nconst logger = createLogger({\n  // ...options\n});\nconst store = createStore(\n  reducer,\n  applyMiddleware(logger)\n)\n"),function(){return function(e){return function(t){return e(t)}}};var u=[];return function(e){var r=e.getState;return function(e){return function(l){if("function"==typeof i&&!i(r,l))return e(l);var c={};u.push(c),c.started=O.now(),c.startedTime=new Date,c.prevState=n(r()),c.action=l;var s=void 0;if(a)try{s=e(l)}catch(e){c.error=o(e)}else s=e(l);c.took=O.now()-c.started,c.nextState=n(r());var d=t.diff&&"function"==typeof f?f(r,l):t.diff;if(x(u,Object.assign({},t,{diff:d})),u.length=0,c.error)throw c.error;return s}}}}var k,j,E=function(e,t){return new Array(t+1).join(e)},A=function(e,t){return E("0",t-e.toString().length)+e},D=function(e){return A(e.getHours(),2)+":"+A(e.getMinutes(),2)+":"+A(e.getSeconds(),2)+"."+A(e.getMilliseconds(),3)},O="undefined"!=typeof performance&&null!==performance&&"function"==typeof performance.now?performance:Date,N="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},P=function(e){if(Array.isArray(e)){for(var t=0,r=Array(e.length);t<e.length;t++)r[t]=e[t];return r}return Array.from(e)},C=[];k="object"===("undefined"==typeof global?"undefined":N(global))&&global?global:"undefined"!=typeof window?window:{},j=k.DeepDiff,j&&C.push(function(){"undefined"!=typeof j&&k.DeepDiff===c&&(k.DeepDiff=j,j=void 0)}),t(n,r),t(o,r),t(i,r),t(a,r),Object.defineProperties(c,{diff:{value:c,enumerable:!0},observableDiff:{value:l,enumerable:!0},applyDiff:{value:h,enumerable:!0},applyChange:{value:d,enumerable:!0},revertChange:{value:g,enumerable:!0},isConflict:{value:function(){return"undefined"!=typeof j},enumerable:!0},noConflict:{value:function(){return C&&(C.forEach(function(e){e()}),C=null),c},enumerable:!0}});var F={E:{color:"#2196F3",text:"CHANGED:"},N:{color:"#4CAF50",text:"ADDED:"},D:{color:"#F44336",text:"DELETED:"},A:{color:"#2196F3",text:"ARRAY:"}},L={level:"log",logger:console,logErrors:!0,collapsed:void 0,predicate:void 0,duration:!1,timestamp:!0,stateTransformer:function(e){return e},actionTransformer:function(e){return e},errorTransformer:function(e){return e},colors:{title:function(){return"inherit"},prevState:function(){return"#9E9E9E"},action:function(){return"#03A9F4"},nextState:function(){return"#4CAF50"},error:function(){return"#F20404"}},diff:!1,diffPredicate:void 0,transformer:void 0},T=function(){var e=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},t=e.dispatch,r=e.getState;return"function"==typeof t||"function"==typeof r?S()({dispatch:t,getState:r}):void console.error("\n[redux-logger v3] BREAKING CHANGE\n[redux-logger v3] Since 3.0.0 redux-logger exports by default logger with default settings.\n[redux-logger v3] Change\n[redux-logger v3] import createLogger from 'redux-logger'\n[redux-logger v3] to\n[redux-logger v3] import { createLogger } from 'redux-logger'\n")};e.defaults=L,e.createLogger=S,e.logger=T,e.default=T,Object.defineProperty(e,"__esModule",{value:!0})});

/* WEBPACK VAR INJECTION */}.call(this, __webpack_require__(/*! ./../../webpack/buildin/global.js */ "./node_modules/webpack/buildin/global.js")))

/***/ }),

/***/ "./node_modules/redux-thunk/es/index.js":
/*!*************************************************************************************************************!*\
  !*** delegated ../../../node_modules/redux-thunk/es/index.js from dll-reference react_23efd8d7d6577d253464 ***!
  \*************************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_23efd8d7d6577d253464 */ "dll-reference react_23efd8d7d6577d253464"))(172);

/***/ }),

/***/ "./node_modules/redux/es/redux.js":
/*!*******************************************************************************************************!*\
  !*** delegated ../../../node_modules/redux/es/redux.js from dll-reference react_23efd8d7d6577d253464 ***!
  \*******************************************************************************************************/
/*! exports provided: __DO_NOT_USE__ActionTypes, applyMiddleware, bindActionCreators, combineReducers, compose, createStore */
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(/*! dll-reference react_23efd8d7d6577d253464 */ "dll-reference react_23efd8d7d6577d253464"))(56);

/***/ }),

/***/ "./node_modules/regenerator-runtime/runtime.js":
/*!*****************************************************!*\
  !*** ./node_modules/regenerator-runtime/runtime.js ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

/**
 * Copyright (c) 2014-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

var runtime = (function (exports) {
  "use strict";

  var Op = Object.prototype;
  var hasOwn = Op.hasOwnProperty;
  var undefined; // More compressible than void 0.
  var $Symbol = typeof Symbol === "function" ? Symbol : {};
  var iteratorSymbol = $Symbol.iterator || "@@iterator";
  var asyncIteratorSymbol = $Symbol.asyncIterator || "@@asyncIterator";
  var toStringTagSymbol = $Symbol.toStringTag || "@@toStringTag";

  function define(obj, key, value) {
    Object.defineProperty(obj, key, {
      value: value,
      enumerable: true,
      configurable: true,
      writable: true
    });
    return obj[key];
  }
  try {
    // IE 8 has a broken Object.defineProperty that only works on DOM objects.
    define({}, "");
  } catch (err) {
    define = function(obj, key, value) {
      return obj[key] = value;
    };
  }

  function wrap(innerFn, outerFn, self, tryLocsList) {
    // If outerFn provided and outerFn.prototype is a Generator, then outerFn.prototype instanceof Generator.
    var protoGenerator = outerFn && outerFn.prototype instanceof Generator ? outerFn : Generator;
    var generator = Object.create(protoGenerator.prototype);
    var context = new Context(tryLocsList || []);

    // The ._invoke method unifies the implementations of the .next,
    // .throw, and .return methods.
    generator._invoke = makeInvokeMethod(innerFn, self, context);

    return generator;
  }
  exports.wrap = wrap;

  // Try/catch helper to minimize deoptimizations. Returns a completion
  // record like context.tryEntries[i].completion. This interface could
  // have been (and was previously) designed to take a closure to be
  // invoked without arguments, but in all the cases we care about we
  // already have an existing method we want to call, so there's no need
  // to create a new function object. We can even get away with assuming
  // the method takes exactly one argument, since that happens to be true
  // in every case, so we don't have to touch the arguments object. The
  // only additional allocation required is the completion record, which
  // has a stable shape and so hopefully should be cheap to allocate.
  function tryCatch(fn, obj, arg) {
    try {
      return { type: "normal", arg: fn.call(obj, arg) };
    } catch (err) {
      return { type: "throw", arg: err };
    }
  }

  var GenStateSuspendedStart = "suspendedStart";
  var GenStateSuspendedYield = "suspendedYield";
  var GenStateExecuting = "executing";
  var GenStateCompleted = "completed";

  // Returning this object from the innerFn has the same effect as
  // breaking out of the dispatch switch statement.
  var ContinueSentinel = {};

  // Dummy constructor functions that we use as the .constructor and
  // .constructor.prototype properties for functions that return Generator
  // objects. For full spec compliance, you may wish to configure your
  // minifier not to mangle the names of these two functions.
  function Generator() {}
  function GeneratorFunction() {}
  function GeneratorFunctionPrototype() {}

  // This is a polyfill for %IteratorPrototype% for environments that
  // don't natively support it.
  var IteratorPrototype = {};
  IteratorPrototype[iteratorSymbol] = function () {
    return this;
  };

  var getProto = Object.getPrototypeOf;
  var NativeIteratorPrototype = getProto && getProto(getProto(values([])));
  if (NativeIteratorPrototype &&
      NativeIteratorPrototype !== Op &&
      hasOwn.call(NativeIteratorPrototype, iteratorSymbol)) {
    // This environment has a native %IteratorPrototype%; use it instead
    // of the polyfill.
    IteratorPrototype = NativeIteratorPrototype;
  }

  var Gp = GeneratorFunctionPrototype.prototype =
    Generator.prototype = Object.create(IteratorPrototype);
  GeneratorFunction.prototype = Gp.constructor = GeneratorFunctionPrototype;
  GeneratorFunctionPrototype.constructor = GeneratorFunction;
  GeneratorFunction.displayName = define(
    GeneratorFunctionPrototype,
    toStringTagSymbol,
    "GeneratorFunction"
  );

  // Helper for defining the .next, .throw, and .return methods of the
  // Iterator interface in terms of a single ._invoke method.
  function defineIteratorMethods(prototype) {
    ["next", "throw", "return"].forEach(function(method) {
      define(prototype, method, function(arg) {
        return this._invoke(method, arg);
      });
    });
  }

  exports.isGeneratorFunction = function(genFun) {
    var ctor = typeof genFun === "function" && genFun.constructor;
    return ctor
      ? ctor === GeneratorFunction ||
        // For the native GeneratorFunction constructor, the best we can
        // do is to check its .name property.
        (ctor.displayName || ctor.name) === "GeneratorFunction"
      : false;
  };

  exports.mark = function(genFun) {
    if (Object.setPrototypeOf) {
      Object.setPrototypeOf(genFun, GeneratorFunctionPrototype);
    } else {
      genFun.__proto__ = GeneratorFunctionPrototype;
      define(genFun, toStringTagSymbol, "GeneratorFunction");
    }
    genFun.prototype = Object.create(Gp);
    return genFun;
  };

  // Within the body of any async function, `await x` is transformed to
  // `yield regeneratorRuntime.awrap(x)`, so that the runtime can test
  // `hasOwn.call(value, "__await")` to determine if the yielded value is
  // meant to be awaited.
  exports.awrap = function(arg) {
    return { __await: arg };
  };

  function AsyncIterator(generator, PromiseImpl) {
    function invoke(method, arg, resolve, reject) {
      var record = tryCatch(generator[method], generator, arg);
      if (record.type === "throw") {
        reject(record.arg);
      } else {
        var result = record.arg;
        var value = result.value;
        if (value &&
            typeof value === "object" &&
            hasOwn.call(value, "__await")) {
          return PromiseImpl.resolve(value.__await).then(function(value) {
            invoke("next", value, resolve, reject);
          }, function(err) {
            invoke("throw", err, resolve, reject);
          });
        }

        return PromiseImpl.resolve(value).then(function(unwrapped) {
          // When a yielded Promise is resolved, its final value becomes
          // the .value of the Promise<{value,done}> result for the
          // current iteration.
          result.value = unwrapped;
          resolve(result);
        }, function(error) {
          // If a rejected Promise was yielded, throw the rejection back
          // into the async generator function so it can be handled there.
          return invoke("throw", error, resolve, reject);
        });
      }
    }

    var previousPromise;

    function enqueue(method, arg) {
      function callInvokeWithMethodAndArg() {
        return new PromiseImpl(function(resolve, reject) {
          invoke(method, arg, resolve, reject);
        });
      }

      return previousPromise =
        // If enqueue has been called before, then we want to wait until
        // all previous Promises have been resolved before calling invoke,
        // so that results are always delivered in the correct order. If
        // enqueue has not been called before, then it is important to
        // call invoke immediately, without waiting on a callback to fire,
        // so that the async generator function has the opportunity to do
        // any necessary setup in a predictable way. This predictability
        // is why the Promise constructor synchronously invokes its
        // executor callback, and why async functions synchronously
        // execute code before the first await. Since we implement simple
        // async functions in terms of async generators, it is especially
        // important to get this right, even though it requires care.
        previousPromise ? previousPromise.then(
          callInvokeWithMethodAndArg,
          // Avoid propagating failures to Promises returned by later
          // invocations of the iterator.
          callInvokeWithMethodAndArg
        ) : callInvokeWithMethodAndArg();
    }

    // Define the unified helper method that is used to implement .next,
    // .throw, and .return (see defineIteratorMethods).
    this._invoke = enqueue;
  }

  defineIteratorMethods(AsyncIterator.prototype);
  AsyncIterator.prototype[asyncIteratorSymbol] = function () {
    return this;
  };
  exports.AsyncIterator = AsyncIterator;

  // Note that simple async functions are implemented on top of
  // AsyncIterator objects; they just return a Promise for the value of
  // the final result produced by the iterator.
  exports.async = function(innerFn, outerFn, self, tryLocsList, PromiseImpl) {
    if (PromiseImpl === void 0) PromiseImpl = Promise;

    var iter = new AsyncIterator(
      wrap(innerFn, outerFn, self, tryLocsList),
      PromiseImpl
    );

    return exports.isGeneratorFunction(outerFn)
      ? iter // If outerFn is a generator, return the full iterator.
      : iter.next().then(function(result) {
          return result.done ? result.value : iter.next();
        });
  };

  function makeInvokeMethod(innerFn, self, context) {
    var state = GenStateSuspendedStart;

    return function invoke(method, arg) {
      if (state === GenStateExecuting) {
        throw new Error("Generator is already running");
      }

      if (state === GenStateCompleted) {
        if (method === "throw") {
          throw arg;
        }

        // Be forgiving, per 25.3.3.3.3 of the spec:
        // https://people.mozilla.org/~jorendorff/es6-draft.html#sec-generatorresume
        return doneResult();
      }

      context.method = method;
      context.arg = arg;

      while (true) {
        var delegate = context.delegate;
        if (delegate) {
          var delegateResult = maybeInvokeDelegate(delegate, context);
          if (delegateResult) {
            if (delegateResult === ContinueSentinel) continue;
            return delegateResult;
          }
        }

        if (context.method === "next") {
          // Setting context._sent for legacy support of Babel's
          // function.sent implementation.
          context.sent = context._sent = context.arg;

        } else if (context.method === "throw") {
          if (state === GenStateSuspendedStart) {
            state = GenStateCompleted;
            throw context.arg;
          }

          context.dispatchException(context.arg);

        } else if (context.method === "return") {
          context.abrupt("return", context.arg);
        }

        state = GenStateExecuting;

        var record = tryCatch(innerFn, self, context);
        if (record.type === "normal") {
          // If an exception is thrown from innerFn, we leave state ===
          // GenStateExecuting and loop back for another invocation.
          state = context.done
            ? GenStateCompleted
            : GenStateSuspendedYield;

          if (record.arg === ContinueSentinel) {
            continue;
          }

          return {
            value: record.arg,
            done: context.done
          };

        } else if (record.type === "throw") {
          state = GenStateCompleted;
          // Dispatch the exception by looping back around to the
          // context.dispatchException(context.arg) call above.
          context.method = "throw";
          context.arg = record.arg;
        }
      }
    };
  }

  // Call delegate.iterator[context.method](context.arg) and handle the
  // result, either by returning a { value, done } result from the
  // delegate iterator, or by modifying context.method and context.arg,
  // setting context.delegate to null, and returning the ContinueSentinel.
  function maybeInvokeDelegate(delegate, context) {
    var method = delegate.iterator[context.method];
    if (method === undefined) {
      // A .throw or .return when the delegate iterator has no .throw
      // method always terminates the yield* loop.
      context.delegate = null;

      if (context.method === "throw") {
        // Note: ["return"] must be used for ES3 parsing compatibility.
        if (delegate.iterator["return"]) {
          // If the delegate iterator has a return method, give it a
          // chance to clean up.
          context.method = "return";
          context.arg = undefined;
          maybeInvokeDelegate(delegate, context);

          if (context.method === "throw") {
            // If maybeInvokeDelegate(context) changed context.method from
            // "return" to "throw", let that override the TypeError below.
            return ContinueSentinel;
          }
        }

        context.method = "throw";
        context.arg = new TypeError(
          "The iterator does not provide a 'throw' method");
      }

      return ContinueSentinel;
    }

    var record = tryCatch(method, delegate.iterator, context.arg);

    if (record.type === "throw") {
      context.method = "throw";
      context.arg = record.arg;
      context.delegate = null;
      return ContinueSentinel;
    }

    var info = record.arg;

    if (! info) {
      context.method = "throw";
      context.arg = new TypeError("iterator result is not an object");
      context.delegate = null;
      return ContinueSentinel;
    }

    if (info.done) {
      // Assign the result of the finished delegate to the temporary
      // variable specified by delegate.resultName (see delegateYield).
      context[delegate.resultName] = info.value;

      // Resume execution at the desired location (see delegateYield).
      context.next = delegate.nextLoc;

      // If context.method was "throw" but the delegate handled the
      // exception, let the outer generator proceed normally. If
      // context.method was "next", forget context.arg since it has been
      // "consumed" by the delegate iterator. If context.method was
      // "return", allow the original .return call to continue in the
      // outer generator.
      if (context.method !== "return") {
        context.method = "next";
        context.arg = undefined;
      }

    } else {
      // Re-yield the result returned by the delegate method.
      return info;
    }

    // The delegate iterator is finished, so forget it and continue with
    // the outer generator.
    context.delegate = null;
    return ContinueSentinel;
  }

  // Define Generator.prototype.{next,throw,return} in terms of the
  // unified ._invoke helper method.
  defineIteratorMethods(Gp);

  define(Gp, toStringTagSymbol, "Generator");

  // A Generator should always return itself as the iterator object when the
  // @@iterator function is called on it. Some browsers' implementations of the
  // iterator prototype chain incorrectly implement this, causing the Generator
  // object to not be returned from this call. This ensures that doesn't happen.
  // See https://github.com/facebook/regenerator/issues/274 for more details.
  Gp[iteratorSymbol] = function() {
    return this;
  };

  Gp.toString = function() {
    return "[object Generator]";
  };

  function pushTryEntry(locs) {
    var entry = { tryLoc: locs[0] };

    if (1 in locs) {
      entry.catchLoc = locs[1];
    }

    if (2 in locs) {
      entry.finallyLoc = locs[2];
      entry.afterLoc = locs[3];
    }

    this.tryEntries.push(entry);
  }

  function resetTryEntry(entry) {
    var record = entry.completion || {};
    record.type = "normal";
    delete record.arg;
    entry.completion = record;
  }

  function Context(tryLocsList) {
    // The root entry object (effectively a try statement without a catch
    // or a finally block) gives us a place to store values thrown from
    // locations where there is no enclosing try statement.
    this.tryEntries = [{ tryLoc: "root" }];
    tryLocsList.forEach(pushTryEntry, this);
    this.reset(true);
  }

  exports.keys = function(object) {
    var keys = [];
    for (var key in object) {
      keys.push(key);
    }
    keys.reverse();

    // Rather than returning an object with a next method, we keep
    // things simple and return the next function itself.
    return function next() {
      while (keys.length) {
        var key = keys.pop();
        if (key in object) {
          next.value = key;
          next.done = false;
          return next;
        }
      }

      // To avoid creating an additional object, we just hang the .value
      // and .done properties off the next function object itself. This
      // also ensures that the minifier will not anonymize the function.
      next.done = true;
      return next;
    };
  };

  function values(iterable) {
    if (iterable) {
      var iteratorMethod = iterable[iteratorSymbol];
      if (iteratorMethod) {
        return iteratorMethod.call(iterable);
      }

      if (typeof iterable.next === "function") {
        return iterable;
      }

      if (!isNaN(iterable.length)) {
        var i = -1, next = function next() {
          while (++i < iterable.length) {
            if (hasOwn.call(iterable, i)) {
              next.value = iterable[i];
              next.done = false;
              return next;
            }
          }

          next.value = undefined;
          next.done = true;

          return next;
        };

        return next.next = next;
      }
    }

    // Return an iterator with no values.
    return { next: doneResult };
  }
  exports.values = values;

  function doneResult() {
    return { value: undefined, done: true };
  }

  Context.prototype = {
    constructor: Context,

    reset: function(skipTempReset) {
      this.prev = 0;
      this.next = 0;
      // Resetting context._sent for legacy support of Babel's
      // function.sent implementation.
      this.sent = this._sent = undefined;
      this.done = false;
      this.delegate = null;

      this.method = "next";
      this.arg = undefined;

      this.tryEntries.forEach(resetTryEntry);

      if (!skipTempReset) {
        for (var name in this) {
          // Not sure about the optimal order of these conditions:
          if (name.charAt(0) === "t" &&
              hasOwn.call(this, name) &&
              !isNaN(+name.slice(1))) {
            this[name] = undefined;
          }
        }
      }
    },

    stop: function() {
      this.done = true;

      var rootEntry = this.tryEntries[0];
      var rootRecord = rootEntry.completion;
      if (rootRecord.type === "throw") {
        throw rootRecord.arg;
      }

      return this.rval;
    },

    dispatchException: function(exception) {
      if (this.done) {
        throw exception;
      }

      var context = this;
      function handle(loc, caught) {
        record.type = "throw";
        record.arg = exception;
        context.next = loc;

        if (caught) {
          // If the dispatched exception was caught by a catch block,
          // then let that catch block handle the exception normally.
          context.method = "next";
          context.arg = undefined;
        }

        return !! caught;
      }

      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        var record = entry.completion;

        if (entry.tryLoc === "root") {
          // Exception thrown outside of any try block that could handle
          // it, so set the completion value of the entire function to
          // throw the exception.
          return handle("end");
        }

        if (entry.tryLoc <= this.prev) {
          var hasCatch = hasOwn.call(entry, "catchLoc");
          var hasFinally = hasOwn.call(entry, "finallyLoc");

          if (hasCatch && hasFinally) {
            if (this.prev < entry.catchLoc) {
              return handle(entry.catchLoc, true);
            } else if (this.prev < entry.finallyLoc) {
              return handle(entry.finallyLoc);
            }

          } else if (hasCatch) {
            if (this.prev < entry.catchLoc) {
              return handle(entry.catchLoc, true);
            }

          } else if (hasFinally) {
            if (this.prev < entry.finallyLoc) {
              return handle(entry.finallyLoc);
            }

          } else {
            throw new Error("try statement without catch or finally");
          }
        }
      }
    },

    abrupt: function(type, arg) {
      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        if (entry.tryLoc <= this.prev &&
            hasOwn.call(entry, "finallyLoc") &&
            this.prev < entry.finallyLoc) {
          var finallyEntry = entry;
          break;
        }
      }

      if (finallyEntry &&
          (type === "break" ||
           type === "continue") &&
          finallyEntry.tryLoc <= arg &&
          arg <= finallyEntry.finallyLoc) {
        // Ignore the finally entry if control is not jumping to a
        // location outside the try/catch block.
        finallyEntry = null;
      }

      var record = finallyEntry ? finallyEntry.completion : {};
      record.type = type;
      record.arg = arg;

      if (finallyEntry) {
        this.method = "next";
        this.next = finallyEntry.finallyLoc;
        return ContinueSentinel;
      }

      return this.complete(record);
    },

    complete: function(record, afterLoc) {
      if (record.type === "throw") {
        throw record.arg;
      }

      if (record.type === "break" ||
          record.type === "continue") {
        this.next = record.arg;
      } else if (record.type === "return") {
        this.rval = this.arg = record.arg;
        this.method = "return";
        this.next = "end";
      } else if (record.type === "normal" && afterLoc) {
        this.next = afterLoc;
      }

      return ContinueSentinel;
    },

    finish: function(finallyLoc) {
      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        if (entry.finallyLoc === finallyLoc) {
          this.complete(entry.completion, entry.afterLoc);
          resetTryEntry(entry);
          return ContinueSentinel;
        }
      }
    },

    "catch": function(tryLoc) {
      for (var i = this.tryEntries.length - 1; i >= 0; --i) {
        var entry = this.tryEntries[i];
        if (entry.tryLoc === tryLoc) {
          var record = entry.completion;
          if (record.type === "throw") {
            var thrown = record.arg;
            resetTryEntry(entry);
          }
          return thrown;
        }
      }

      // The context.catch method must only be called with a location
      // argument that corresponds to a known catch block.
      throw new Error("illegal catch attempt");
    },

    delegateYield: function(iterable, resultName, nextLoc) {
      this.delegate = {
        iterator: values(iterable),
        resultName: resultName,
        nextLoc: nextLoc
      };

      if (this.method === "next") {
        // Deliberately forget the last sent value so that we don't
        // accidentally pass it on to the delegate.
        this.arg = undefined;
      }

      return ContinueSentinel;
    }
  };

  // Regardless of whether this script is executing as a CommonJS module
  // or not, return the runtime object so that we can declare the variable
  // regeneratorRuntime in the outer scope, which allows this module to be
  // injected easily by `bin/regenerator --include-runtime script.js`.
  return exports;

}(
  // If this script is executing as a CommonJS module, use module.exports
  // as the regeneratorRuntime namespace. Otherwise create a new empty
  // object. Either way, the resulting object will be used to initialize
  // the regeneratorRuntime variable at the top of this file.
   true ? module.exports : undefined
));

try {
  regeneratorRuntime = runtime;
} catch (accidentalStrictMode) {
  // This module should not be running in strict mode, so the above
  // assignment should always work unless something is misconfigured. Just
  // in case runtime.js accidentally runs in strict mode, we can escape
  // strict mode using a global Function call. This could conceivably fail
  // if a Content Security Policy forbids using Function, but in that case
  // the proper solution is to fix the accidental strict mode problem. If
  // you've misconfigured your bundler to force strict mode and applied a
  // CSP to forbid Function, and you're not willing to fix either of those
  // problems, please detail your unique predicament in a GitHub issue.
  Function("r", "regeneratorRuntime = r")(runtime);
}


/***/ })

}]);
//# sourceMappingURL=0.chunk.js.map