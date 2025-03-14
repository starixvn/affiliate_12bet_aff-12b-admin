import React from 'react';

import { Input } from 'reactstrap';
import { commissionValueList, format } from '../../../modules/constant';

const money = {
    AllowedMaxValue: 100000000000,
    AllowedMinValue: -100000000000
}

const regExp = {
    acceptAmountFormat: "^\\-?\\d*$",
    validAmount: "^\\-?\\d+$"
}

const regexAcceptAmountFormat = new RegExp(regExp.acceptAmountFormat);
const regexValidAmount = new RegExp(regExp.validAmount);

export default class TierSettingItem extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    handleChangeAmount = (event) => {
        const { name, value } = event.target;
        const amount = value.replace(/,/g, '');

        if (regexAcceptAmountFormat.test(amount)) {
            if (regexValidAmount.test(amount)) {
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

    isValidAmount = (amount) => {
        return regexValidAmount.test(amount);
    }

    handleChangeMinQm = (event) => {
        const target = event.target;
        const value = Number(target.value.replace(/,/g, ''));
        const name = target.name;

        const { data, onChange } = this.props;

        const newItem = { [name]: value || 0 };

        onChange && onChange(data.TierId, newItem);
    }

    render() {
        const { data, handleClick, btnText } = this.props;
        const { TierId, MinAmount, MinQm, Commission } = data;

        const isValidAmount = this.isValidAmount(MinAmount);

        return (
            <tr>
                <td className="text-center">{TierId}</td>
                <td>
                    <Input className={isValidAmount ? "" : "has-error"} type="text" name="MinAmount" id={"MinAmount" + TierId}
                        value={isValidAmount ? format(Number(MinAmount)) : MinAmount} onChange={this.handleChangeAmount} />
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