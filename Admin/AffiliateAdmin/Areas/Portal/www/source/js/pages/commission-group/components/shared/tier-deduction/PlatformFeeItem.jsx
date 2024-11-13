import React from "react";
import TierSetting from './TierSetting';
import MultipleSelect from "../../../../../components/multiple-select";
import { Col, Form, FormGroup, Label } from 'reactstrap';
import DeductedPercentageForm from '../deduction/DeductedPercentageGroup';
import { ONLY_ONE_ITEM, GGR, Turnover, commissionValueList, killRateValueList } from '../../../modules/constant';

export default class PlatformFeeItem extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange = (selectedItem) => {
        selectedItem && this.props.onSelectProduct(selectedItem);
    }

    handleChangeComm = (event, propName) => {
        const { name, value } = event.target;
        this.props.onChangeCommission({ [name]: Number(value) }, propName);
    }

    render() {
        const { data, handleClick, isOnlyOnePlatformFeeRow, isFirst, isTurnover, index } = this.props;

        const { productList, royaltyFee, killRate, groupProductId } = data;
        const isValid = (isFirst && isOnlyOnePlatformFeeRow) || productList.filter(p => !p.Checked).length < productList.length;
        const isDisableAddButton = isFirst && (productList.length === ONLY_ONE_ITEM);
        const btnText = isFirst ? (<><span>Add Product Group</span><span>+</span></>) : (<><span>Remove Product Group</span><span>-</span></>);

        return (
            <div className="my-2">
                <FormGroup row className="align-items-center">
                    <Col sm={3}>
                        <span className="group-index">{index}.</span>
                        <div className={`select-product ${isValid ? "" : "is-invalid"}`}>
                            <MultipleSelect
                                dataSource={productList}
                                keyField="ProductId"
                                valueField="ProductName"
                                statusField="Checked"
                                hasAllOption={false}
                                hasInputOption={!isOnlyOnePlatformFeeRow}
                                maxDisplayItemCount={1}
                                noneSelectedLabel={isValid ? "None" : "Please Select"}
                                onChange={this.handleChange}
                                isDisable={isOnlyOnePlatformFeeRow} />
                        </div>
                    </Col>
                    <Col sm={5} className="pl-0">
                        <Form inline>
                            <Label className="font-weight-bold pr-2">Royalty Fee:</Label>
                            <DeductedPercentageForm
                                value={royaltyFee}
                                label={GGR}
                                valueList={commissionValueList}
                                onChange={(e) => this.handleChangeComm(e, 'PlatformFeeList')} />
                        </Form>
                    </Col>
                    {isTurnover && <Col sm={4} className="pl-0">
                        <Form inline>
                            <Label className="font-weight-bold pr-2">Kill Rate:</Label>
                            <DeductedPercentageForm
                                value={killRate}
                                label={Turnover}
                                valueList={killRateValueList}
                                onChange={(e) => this.handleChangeComm(e, 'KillRateFeeList')} />
                        </Form>
                    </Col>}
                </FormGroup>
                <TierSetting
                    groupProductId={groupProductId}
                    handleClick={handleClick}
                    isDisableAddButton={isDisableAddButton}
                    btnText={btnText}
                    isTurnover={isTurnover}
                />
            </div>
        )
    }
}