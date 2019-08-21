import React from "react";
import MultipleSelect from "../../../../../components/multiple-select";
import { Col, FormGroup, Input, Label, Form, FormFeedback } from 'reactstrap';
import { commissionValueList } from '../../../modules/constant';
import { formatDecimal } from '../../../../../modules/numberFormat';

export default class PlatformFeeItem extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange = (selectedItem) => {
        const { onSelectProduct } = this.props;
        selectedItem && onSelectProduct && onSelectProduct(selectedItem);
    }

    handleChangeComm = (event) => {
        const { name, value } = event.target;
        this.props.onChangeCommission({ [name]: Number(value) });
    }

    render() {
        const { data, handleClick, btnText, isInserting } = this.props;

        const { productList, commission, nextCommission } = data;
        const isValid = productList.filter(p => !p.Checked).length < productList.length;

        return (
            <FormGroup row>
                <Col sm={4}>
                    <div className={`select-product ${isValid ? "" : "is-invalid"}`}>
                        <MultipleSelect
                            dataSource={productList}
                            keyField="ProductId"
                            valueField="ProductName"
                            statusField="Checked"
                            hasAllOption={false}
                            maxDisplayItemCount={1}
                            noneSelectedLabel="Please Select"
                            onChange={this.handleChange} />
                        <FormFeedback>At least 1 product must be selected per group</FormFeedback>
                    </div>
                </Col>
                {
                    isInserting ? (
                        <Col sm={2} className="select-comm">
                            <select name="DeductedPercentage" className="form-control" value={formatDecimal(commission)}
                                onChange={this.handleChangeComm}>
                                {
                                    commissionValueList.map((item, i) => {
                                        return (<option key={`platformfee-comm-${i}`} value={item}>{item}</option>);
                                    })
                                }
                            </select>
                        </Col>
                    ) :
                        (
                            <Col sm={5} className="select-comm">
                                <Form inline>
                                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                                        <Label className="mr-sm-2">Current: {formatDecimal(commission || 0)} | Next month: </Label>
                                    </FormGroup>
                                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                                        <select name="NextDeductedPercentage" className="form-control"
                                            value={formatDecimal(nextCommission || 0)}
                                            onChange={this.handleChangeComm} style={{ width: 60 }}>
                                            {
                                                commissionValueList.map((item, i) => {
                                                    return (<option key={`platformfee-nextmonth-comm-${i}`} value={item}>{item}</option>);
                                                })
                                            }
                                        </select>
                                    </FormGroup>
                                </Form>
                            </Col>
                        )
                }

                <Col sm={2}>
                    <Input plaintext tag="p">| Gross Win Loss |</Input>
                </Col>
                <Col sm={1}>
                    <button type="button" className="btn btn-action" onClick={handleClick}>{btnText}</button>
                </Col>
            </FormGroup>
        )
    }
}