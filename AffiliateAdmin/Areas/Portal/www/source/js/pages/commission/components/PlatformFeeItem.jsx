import React, { Component } from "react";

import MultipleSelect from "../../../components/multiple-select";

export default class PlatformFeeItem extends Component {
    constructor(props) {
        super(props);
    }

    onSelect = (selectedItem, newDataSource) => {
        selectedItem && this.props.onSelect && this.props.onSelect(selectedItem, newDataSource);
    }

    render() {
        const { productList, commission } = this.props.data;
        return (
            <li className="inline">
                <div className="select-product">
                    <MultipleSelect
                        dataSource={productList}
                        keyField="code"
                        valueField="name"
                        statusField="checked"
                        hasAllOption={false}
                        maxDisplayItemCount={1}
                        noneSelectedLabel="Please Select"
                        onChange={this.onSelect} />
                </div>
                <select className="select-percent">
                    <option>{commission}</option>
                </select>
                <span className="sign">x</span>
                <span className="unit-name">| Gross Win Loss |</span>
                {this.props.children}
            </li>
        )
    }
}