import React, { Component } from "react";
import { Col, Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';

import PlatformFeeItem from "./PlatformFeeItem";
import { deepClone } from "../../../modules/utils";

const productList = [
    { code: "SB", name: "Sportsbook", checked: true },
    { code: "CS", name: "Playtech Casino", checked: true },
    { code: "FW", name: "Fishing World", checked: true },
    { code: "MGC", name: "Microgaming - Live Casino", checked: true },
    { code: "AB", name: "Allbet", checked: true }
];

const platformFeeSettings = [
    {
        productList: [...productList],
        commission: 0.17
    }
];

export default class DeductionSetting extends Component {
    constructor(props) {
        super(props);

        this.state = {
            platformFeeSettings: platformFeeSettings
        }
    }

    handleAdd = () => {
        const platformFeeSettings = this.state.platformFeeSettings;
        const newProductList = productList.map(item => {
            return Object.assign({}, item, { checked: !item.checked });
        });

        this.setState({
            platformFeeSettings: [...platformFeeSettings, { productList: newProductList, commission: 0.17 }]
        });
    }

    handleRemove = (index) => {
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);

        const rootProductList = platformFeeSettings[0].productList;
        const checkedProductList = platformFeeSettings[index].productList.filter(p => p.checked);

        for (let i = 0; i < checkedProductList.length; i++) {
            const code = checkedProductList[i].code;
            const rootProductItem = rootProductList.find(p => p.code === code);

            if (rootProductItem) {
                rootProductItem.checked = true;
            }
        }

        platformFeeSettings.splice(index, 1);

        this.setState({ platformFeeSettings });
    }

    handleSelect = (index, selectedItem, newDataSource) => {
        const isChecked = selectedItem.checked;
        const platformFeeSettings = deepClone(this.state.platformFeeSettings);
        const settingCount = platformFeeSettings.length;

        if (settingCount > 1) {
            const currentproductList = platformFeeSettings[index].productList;
            const currentProductItem = currentproductList.find(p => p.code === selectedItem.code);
            currentProductItem.checked = isChecked;

            if (isChecked) {
                for (let i = 0; i < platformFeeSettings.length; i++) {
                    if (i !== index) {
                        const productList = platformFeeSettings[i].productList;
                        const productItem = productList.find(p => p.code === selectedItem.code && p.checked === isChecked);

                        if (productItem) {
                            productItem.checked = !isChecked;
                            break;
                        }
                    }
                }
            }
            else {
                const productList = platformFeeSettings[0].productList;
                const productItem = productList.find(p => p.code === selectedItem.code);
                productItem.checked = !isChecked;
            }
        }

        this.setState({ platformFeeSettings });
    }

    render() {
        return (
            <fieldset>
                <legend><h6 className="font-weight-bold">Deductions</h6></legend>
                <p className="font-weight-bold">Platform Fee %</p>
                <div className="platformfee-group">
                    {
                        this.state.platformFeeSettings.map((item, i) => {
                            const isFirst = i === 0;
                            const handleClick = isFirst ? this.handleAdd : () => this.handleRemove(i);

                            return (
                                <PlatformFeeItem data={item} key={`fee-${i}`}
                                    onSelect={(selectedItem, newDataSource) => this.handleSelect(i, selectedItem, newDataSource)}
                                    handleClick={handleClick}
                                    btnText={isFirst ? '+' : '-'}>
                                </PlatformFeeItem>
                            )
                        })
                    }
                </div>

                <FormGroup row>
                    <Label sm={4} className="font-weight-bold">Operational Fee%</Label>
                  
                    <Col sm={{ size: 2, offset: 1 }}>
                        <select className="form-control">
                            <option>0.07</option>
                        </select>
                    </Col>
                    <Col sm={1}>
                        <Input plaintext>x</Input>
                    </Col>
                    <Col sm={3}>
                        <Input plaintext>| Gross Win Loss |</Input>
                    </Col>
                </FormGroup>

                <FormGroup row>
                    <Label sm={4} className="font-weight-bold">Banking Fee%</Label>
                  
                    <Col sm={{ size: 2, offset: 1 }}>
                        <select className="form-control">
                            <option>0.02</option>
                        </select>
                    </Col>
                    <Col sm={1}>
                        <Input plaintext>x</Input>
                    </Col>
                    <Col sm={3}>
                        <Input plaintext>(Deposit + Withdrawal)</Input>
                    </Col>
                </FormGroup>
            </fieldset>
        )
    }
}