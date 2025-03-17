import React from 'react';

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { commissionValueList, format, money, NEGATIVE_INFINITY_SYMBOL, regexAcceptNegativeAmountFormat, regexValidNegativeAmount } from '../../../modules/constant';

export default class TierSettingItem extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    handleChangeAmount = (event) => {
        const { name, value } = event.target;
        const amount = value.replace(/,/g, '');

        if (regexAcceptNegativeAmountFormat.test(amount)) {
            if (regexValidNegativeAmount.test(amount)) {
                const amountNumber = Number(amount);

                if (amountNumber < money.AllowedMinValue || amountNumber > money.AllowedMaxValue) {
                    return;
                }
            }

            const { data, onChange } = this.props;

            const newItem = { [name]: amount };

            onChange && onChange(data.TierId, newItem);
        }
    }

    handleChangeMinQm = (event) => {
        const target = event.target;
        const value = Number(target.value.replace(/,/g, ''));
        const name = target.name;

        const { data, onChange } = this.props;

        const newItem = { [name]: value || 0 };

        onChange && onChange(data.TierId, newItem);
    }

    handleGetMinAmount = (minAmount, isValidAmount) => {
        return isValidAmount ? format(Number(minAmount)) : minAmount;
    }

    handleCheckNegativeInfinitySymbol = (event) => {
        const isChecked = event.target.checked;
        const { data, onChange } = this.props;

        const newItem = isChecked ? { MinAmount: money.AllowedMinValue } : { MinAmount: 0 };

        onChange && onChange(data.TierId, newItem);
    }

    render() {
        const { data, handleClick, btnText, isDisabledNegativeInfinity } = this.props;
        const { TierId, MinAmount, MinQm, Commission } = data;

        const isValidAmount = regexValidNegativeAmount.test(MinAmount);
        const isDisabled = MinAmount == money.AllowedMinValue;

        return (
            <tr>
                <td className="text-center">{TierId}</td>
                <td>
                    <Form inline>
                        <FormGroup check className="mr-sm-1">
                            <Label check>
                                <Input type="checkbox" checked={isDisabled}
                                    onChange={this.handleCheckNegativeInfinitySymbol} disabled={isDisabledNegativeInfinity} />{NEGATIVE_INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                        <Input className={isValidAmount ? "form-input-infinity" : "form-input-infinity has-error"} type="text" name="MinAmount" id={"MinAmount" + TierId}
                            disabled={isDisabled} value={isDisabled ? NEGATIVE_INFINITY_SYMBOL : this.handleGetMinAmount(MinAmount, isValidAmount)} onChange={this.handleChangeAmount} />
                    </Form>
                </td>
                <td>
                    <Input type="text" name="MinQm" id={"MinQm" + TierId} value={format(MinQm)} onChange={this.handleChangeMinQm} />
                </td>
                <td>
                    <select className="form-control" name="Commission"
                        onChange={this.handleChange} value={format(Commission || 0, 3)}>
                        {
                            Object.keys(commissionValueList).map((key, i) => {
                                return (<option key={`comm-${i}`} value={key}>{commissionValueList[key]}</option>);
                            })
                        }
                    </select>
                </td>
                <td className="text-center">
                    <button type="button" className="btn btn-action" onClick={handleClick}>{btnText}</button>
                </td>
            </tr>
        )
    }
}