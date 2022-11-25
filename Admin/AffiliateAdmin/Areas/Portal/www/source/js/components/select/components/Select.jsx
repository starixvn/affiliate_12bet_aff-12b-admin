import React, { Component, createRef } from 'react';
import PropTypes from 'prop-types';

import SelectLabel from './SelectLabel';
import SelectOptionList from './SelectOptionList';

import { KEY_NAME, VALUE_NAME } from "../libs/constants";


export default class Select extends Component {
    constructor(props) {
        super(props);
        this.id = `select-${props.id}`;
        this.selectOptionsRef = createRef();

        const { dataSource, keyField, valueField, selectedKey } = props;

        const selectedItem = dataSource.filter(i => i[keyField] == selectedKey)[0] || {};

        this.state = {
            showOptionList: false,
            inputOption: '',
            selectedItem: {
                key: selectedItem[keyField],
                value: selectedItem[valueField]
            }
        }
    }

    static propTypes = {
        id: PropTypes.string.isRequired,
        dataSource: PropTypes.arrayOf(PropTypes.object).isRequired,
        keyField: PropTypes.string,
        valueField: PropTypes.string,
        selectedKey: PropTypes.oneOfType([PropTypes.number, PropTypes.string]).isRequired,
        onChange: PropTypes.func,
        hasInputOption: PropTypes.bool,
        isDisable: PropTypes.bool
    }

    static defaultProps = {
        keyField: KEY_NAME,
        valueField: VALUE_NAME,
        hasInputOption: false,
        isDisable: false
    }

    componentWillUnmount() {
        document.removeEventListener('click', this._handleDocumentClick)
    }

    onChangeHandler = (selectedItem) => {
        document.removeEventListener('click', this._handleDocumentClick);

        this.setState({ selectedItem, showOptionList: false, inputOption: '' })

        this._callBackToParent(selectedItem[KEY_NAME]);
    }

    onToggle = () => {
        this.setState((prevState) => {
            const showOptionList = !prevState.showOptionList;

            showOptionList ? document.addEventListener('click', this._handleDocumentClick) : document.removeEventListener('click', this._handleDocumentClick)

            return { showOptionList, inputOption: '' }
        })
    }

    _handleChangeInput = (event) => {
        const { target } = event;

        this.setState({ [target.name]: target.value });
    }

    _callBackToParent(selectedKey) {
        this.props.onChange && this.props.onChange(selectedKey);
    }

    _convertDataSource(inputOption) {
        const { keyField, valueField, dataSource } = this.props;

        const dataSourceAfterFilter = dataSource.filter(i => i[valueField].toLowerCase().includes(inputOption || ''));

        return dataSourceAfterFilter.map(item => {
            return {
                key: item[keyField],
                value: item[valueField]
            }
        })
    }

    _handleDocumentClick = (event) => {
        const clickOutside = this.wrapper && !this.wrapper.contains(event.target);

        if (clickOutside && this.state.showOptionList) {
            document.removeEventListener('click', this._handleDocumentClick);
            this.setState({ showOptionList: false, inputOption: '' });
        }
    }

    render() {
        const { inputOption, selectedItem, showOptionList } = this.state;
        const { isDisable, hasInputOption } = this.props;
        const dataSource = this._convertDataSource(inputOption.trim().toLowerCase());

        return (
            <div className="select-container" id={this.id} key={this.id} ref={element => this.wrapper = element}>
                <SelectLabel
                    id={this.id}
                    selectedValue={selectedItem.value}
                    onToggle={this.onToggle}
                    isDisable={isDisable} />

                {
                    showOptionList && (
                        <div className="select-options-container">
                            {
                                hasInputOption &&
                                <ul className="select-options">
                                    <input type="text" name="inputOption" id={`inputOption-${this.id}`} value={inputOption} className="w-100" onChange={this._handleChangeInput} />
                                </ul>
                            }

                            <SelectOptionList
                                id={this.id}
                                ref={this.selectOptionsRef}
                                dataSource={dataSource}
                                selectedKey={selectedItem.key}
                                onChange={this.onChangeHandler} />
                        </div>
                    )
                }
            </div>
        )
    }
}