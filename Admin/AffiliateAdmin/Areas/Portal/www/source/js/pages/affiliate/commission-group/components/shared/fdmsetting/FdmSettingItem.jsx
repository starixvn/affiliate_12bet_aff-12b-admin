import React from 'react';

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { INFINITY_SYMBOL, INFINITY_AM, INFINITY_QM, commissionValueList } from '../../../libs/constant';
import { formatDecimal, format } from '../../../../../../modules/numberFormat';

export default class FdmSettingItem extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const target = event.target;
        const value = Number(target.value.replace(/,/g, ''));
        const name = target.name;
        const { data, onChange } = this.props;

        const newItem = { [name]: value || 0 };

        onChange && onChange(data.FdmId, newItem);
    }

    handleCheck = (event) => {
        const isChecked = event.target.checked;
        const { data, onChange } = this.props;

        const newState = isChecked ?
            Object.assign({}, data, { MaxNewFdm: INFINITY_QM, MaxFdmDeposit: INFINITY_AM }) :
            Object.assign({}, data, { MaxNewFdm: data.MinNewFdm, MaxFdmDeposit: data.MinFdmDeposit });

        onChange && onChange(data.FdmId, newState);
    }

    render() {
        const { data, onRemove, isDisabledInfinity } = this.props;
        const { FdmId, MinNewFdm, MaxNewFdm, MinFdmDeposit, MaxFdmDeposit, Commission, MaxExtraComm } = data;
        const isDisabled = MaxFdmDeposit === INFINITY_AM || MaxNewFdm === INFINITY_QM;

        return (
            <tr>
                <td>
                    <Input type="text" name="MinNewFdm" id="MinNewFdm"
                        value={format(MinNewFdm)} onChange={this.handleChange} />
                </td>
                <td>
                    <Form inline>
                        <Input type="text" name="MaxNewFdm" id="MaxNewFdm" className="form-max" disabled={isDisabled}
                            value={isDisabled ? INFINITY_SYMBOL : format(MaxNewFdm)} onChange={this.handleChange} />
                        <FormGroup check className="ml-sm-1">
                            <Label check>
                                <Input type="checkbox" checked={isDisabled}
                                    onChange={this.handleCheck} disabled={isDisabledInfinity} />{INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                    </Form>
                </td>
                <td>
                    <Input type="text" name="MinFdmDeposit" id="MinFdmDeposit" value={format(MinFdmDeposit)} onChange={this.handleChange} />
                </td>
                <td>
                    <Form inline>
                        <Input type="text" name="MaxFdmDeposit" id="MaxFdmDeposit" className="form-max" disabled={isDisabled}
                            value={isDisabled ? INFINITY_SYMBOL : format(MaxFdmDeposit)} onChange={this.handleChange} />
                        <FormGroup check className="ml-sm-1">
                            <Label check>
                                <Input type="checkbox" checked={isDisabled}
                                    onChange={this.handleCheck} disabled={isDisabledInfinity} />{INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                    </Form>
                </td>
                <td>
                    <select className="form-control" name="Commission"
                        onChange={this.handleChange} value={formatDecimal(Commission)}>
                        {
                            commissionValueList.map((item, i) => {
                                return (<option key={`comm-${FdmId}-${i}`} value={item}>{item}</option>);
                            })
                        }
                    </select>
                </td>
                <td>
                    <Input type="text" name="MaxExtraComm" id="MaxExtraComm"
                        value={format(MaxExtraComm)} onChange={this.handleChange} />
                </td>
                <td className="text-center">
                    <button className="btn btn-link" onClick={onRemove}><span className="oi oi-trash text-danger"></span></button>
                </td>
            </tr>
        )
    }
}