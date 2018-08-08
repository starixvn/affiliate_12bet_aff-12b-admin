import React from "react";
import MultipleSelect from "../../../components/multiple-select";
import { Col, FormGroup, Input } from 'reactstrap';
import { commissionValueList } from '../modules/constant';

export default class PlatformFeeItem extends React.Component {
    constructor(props) {
        super(props);

        this.selectedCommission = '0.00';
    }

    handleChange = (selectedItem, newDataSource) => {
        const { onSelectProduct } = this.props;
        selectedItem && onSelectProduct && onSelectProduct(selectedItem);
    }

    handleChangeComm = (event) => {
        this.selectedCommission = event.target.value;
        this.props.onChangeCommission(Number(this.selectedCommission));
    }

    render() {
        const { data, handleClick, btnText } = this.props;

        const { productList } = data;
        return (
            <FormGroup row>
                <Col sm={1}><p className="text-right bullet"><span>&bull;</span></p></Col>
                <Col sm={4}>
                    <div className="select-product">
                        <MultipleSelect
                            dataSource={productList}
                            keyField="productId"
                            valueField="productName"
                            statusField="checked"
                            hasAllOption={false}
                            maxDisplayItemCount={1}
                            noneSelectedLabel="Please Select"
                            onChange={this.handleChange} />
                    </div>
                </Col>
                <Col sm={2}>
                    <select className="form-control select-percent" value={this.selectedCommission}
                        onChange={this.handleChangeComm}>
                        {
                            commissionValueList.map((item, i) => {
                                return (<option key={`platformfee-comm-${i}`} value={item}>{item}</option>);
                            })
                        }
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
}