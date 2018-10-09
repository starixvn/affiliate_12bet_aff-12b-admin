import React, { Component } from 'react';
import PropTypes from 'prop-types';

import axios from 'axios';
import SuggestionList from './SuggestionList'

const DELAY_TIME_MILLISECOND = 300;

function debounce(func, wait) {
    let timeout;
    return function () {
        const context = this,
            args = arguments;

        const later = () => {
            func.apply(context, args);
        };

        clearTimeout(timeout);
        timeout = setTimeout(later, wait || DELAY_TIME_MILLISECOND);
    };
}

export default class AutoComplete extends Component {
    constructor(props) {
        super(props);

        this.state = {
            suggestions: null,
            selectedIndex: -1
        }

        this.input = React.createRef();
        this.wrapper = React.createRef();
        this.cacheData = {};

        this.onChange = debounce(this._onChange).bind(this);
    }

    static propTypes = {
        url: PropTypes.string,
        onSelect: PropTypes.func,
        onInput: PropTypes.func,
        responseDataField: PropTypes.string,
        cache: PropTypes.bool,
        queryParamName: PropTypes.string,
        htmlAttributes: PropTypes.object,
        characterCountToSearch: PropTypes.number,
        value: PropTypes.string
    }

    static defaultProps = {
        url: null,
        cache: true,
        queryParamName: "query",
        characterCountToSearch: 2
    }

    componentDidMount() {
        document.addEventListener('click', this._handleDocumentClick)
    }

    componentWillUnmount() {
        document.removeEventListener('click', this._handleDocumentClick)
    }

    componentDidUpdate(prevProps) {
        if (this.input.current && prevProps.value !== this.props.value && this.props.value !== this.input.current.value) {
            this.input.current.value = this.props.value;
        }
    }

    _handleDocumentClick = (event) => {
        const clickOutside = this.wrapper.current && !this.wrapper.current.contains(event.target);

        if (clickOutside && this.isShowSuggestionList) {
            this._clearState();
        }
    }

    get isShowSuggestionList() {
        return this.state.suggestions && this.state.suggestions.length > 0;
    }

    _onKeyDown = (event) => {
        if (!this.isShowSuggestionList) {
            return;
        }

        const selectedIndex = this.state.selectedIndex;
        switch (event.keyCode) {
            case 38: //up
                if (selectedIndex > 0) {
                    this.setState({ selectedIndex: selectedIndex - 1 });
                }
                break;
            case 40: //down
                if (selectedIndex < this.state.suggestions.length - 1) {
                    this.setState({ selectedIndex: selectedIndex + 1 });
                }
                break;
            case 13: //enter
                if (selectedIndex > -1) {
                    const selectedItem = this.state.suggestions[selectedIndex];
                    this._onSelect(selectedItem);
                }
                break;
        }
    }

    _setStateWithCaching(value, suggestionList) {
        if (this.props.cache) {
            const cacheKey = value.toLowerCase();
            this.cacheData[cacheKey] = suggestionList;
        }

        this.setState({
            suggestions: suggestionList
        });
    }

    _clearState = () => {
        this.setState({ suggestions: null, selectedIndex: -1 });
    }

    _onSelect = (selectedItem) => {
        const { onSelect } = this.props;
        onSelect && onSelect(selectedItem);

        this._clearState();
    }

    _getDataByUrl(url, value) {
        const { responseDataField, queryParamName } = this.props;
        const apiUrl = `${url}?${queryParamName}=${value}`;

        axios.get(apiUrl, {
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        }).then((response) => {
            var data = responseDataField ? response.data[responseDataField] : response.data;
            this._setStateWithCaching(value, data);
        });
    }

    _onChange = (event) => {
        const { characterCountToSearch, onInput } = this.props;
        const value = this.input.current.value;
        onInput && onInput(value);

        if (!value || value.length < characterCountToSearch) {
            this._clearState();
            return;
        }

        const cachedData = this.cacheData[value];
        if (cachedData) {
            this.setState({ suggestions: cachedData });
            return;
        }

        const { url } = this.props;
        if (url) {
            this._getDataByUrl(url, value);
        }
    }

    render() {
        return (
            <div className="ac-container">
                <input type="text" className="input-text" {...this.props.htmlAttributes}
                    onChange={this.onChange} onKeyDown={this._onKeyDown}
                    ref={this.input} />

                {this.isShowSuggestionList &&
                    <div ref={this.wrapper}>
                        <SuggestionList selectedIndex={this.state.selectedIndex}
                            width={this.input.current.offsetWidth}
                            dataList={this.state.suggestions}
                            onSelect={this._onSelect} />
                    </div>}
            </div>
        );
    }
}