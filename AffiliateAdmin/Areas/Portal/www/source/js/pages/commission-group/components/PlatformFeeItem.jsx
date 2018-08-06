import React from "react";
import MultipleSelect from "../../../components/multiple-select";

const PlatformFeeItem = (props) => {
    const { onSelect, data, children } = props;

    const handleChange = (selectedItem, newDataSource) => {
        selectedItem && onSelect && onSelect(selectedItem, newDataSource);
    }

    const { productList, commission } = data;

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
                    onChange={handleChange} />
            </div>
            <select className="select-percent">
                <option>{commission}</option>
            </select>
            <span className="sign">x</span>
            <span className="unit-name">| Gross Win Loss |</span>
            {children}
        </li>
    )
}

export default PlatformFeeItem;