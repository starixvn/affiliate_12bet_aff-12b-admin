import React from "react";
import MultipleSelect from "../../../components/multiple-select";
import { Col, FormGroup, Label, Input, FormText } from 'reactstrap';

const PlatformFeeItem = (props) => {
    const { onSelect, data, handleClick, btnText } = props;

    const handleChange = (selectedItem, newDataSource) => {
        selectedItem && onSelect && onSelect(selectedItem, newDataSource);
    }

    const { productList, commission } = data;

    return (
        <FormGroup row>
            <Col sm={1}><p className="text-right bullet"><span>&bull;</span></p></Col>
            <Col sm={4}>
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
            </Col>
            <Col sm={2}>
                <select className="form-control select-percent">
                    <option>{commission}</option>
                </select>
            </Col>
            <Col sm={1}>
                <Input plaintext>x</Input>
            </Col>
            <Col sm={3}>
                <Input plaintext>| Gross Win Loss |</Input>
            </Col>
            <Col sm={1}>
                <button type="button" className="btn btn-action" onClick={handleClick}>{btnText}</button>
            </Col>
        </FormGroup>
    )
}

export default PlatformFeeItem;