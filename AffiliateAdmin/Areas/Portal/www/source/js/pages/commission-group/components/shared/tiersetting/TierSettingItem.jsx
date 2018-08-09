import React from 'react';

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { INFINITY_SYMBOL, commissionValueList } from '../../../modules/constant';
import { formatDecimal, format } from '../../../../../modules/numberFormat';

export default class TierSettingItem extends React.Component {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const target = event.target;
        const isCheckbox = target.type === 'checkbox';
        const value = isCheckbox ? target.checked : Number(target.value.replace(/,/g, ''));
        const name = target.name;
        const { index, onChange } = this.props;

        const newItem = isCheckbox ? { 'infinityAm': value, 'infinityQm': value } : { [name]: value || 0 };

        onChange && onChange(index, newItem);
    }

    render() {
        const { data, index, onRemove, isDisabledInfinity } = this.props;
        const isDisabled = data.infinityAm || data.infinityQm;
        const isDisabledMinAmount = index > 0;

        return (
            <tr>
                <td className="text-center">{index + 1}</td>
                <td>
                    <Input type="text" name="minAmount" id="minAmount" disabled={isDisabledMinAmount} 
                        value={format(data.minAmount)} onChange={this.handleChange} />
                </td>
                <td>
                    <Form inline>
                        <Input type="text" name="maxAmount" id="maxAmount" className="form-max" disabled={isDisabled} 
                            value={isDisabled ? INFINITY_SYMBOL : format(data.maxAmount)} onChange={this.handleChange} />
                        <FormGroup check className="ml-sm-1">
                            <Label check>
                                <Input type="checkbox" name="infinityAm" checked={isDisabled} 
                                    onChange={this.handleChange} disabled={isDisabledInfinity} />{INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                    </Form>
                </td>
                <td>
                    <Input type="text" name="minQm" id="minQm" value={format(data.minQm)} onChange={this.handleChange} />
                </td>
                <td>
                    <Form inline>
                        <Input type="text" name="maxQm" id="maxQm" className="form-max" disabled={isDisabled} 
                            value={isDisabled ? INFINITY_SYMBOL : format(data.maxQm)} onChange={this.handleChange} />
                        <FormGroup check className="ml-sm-1">
                            <Label check>
                                <Input type="checkbox" name="infinityQm" checked={isDisabled} 
                                    onChange={this.handleChange} disabled={isDisabledInfinity} />{INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                    </Form>
                </td>
                <td>
                    <select className="form-control" name="commission" onChange={this.handleChange} value={formatDecimal(data.commission)}>
                        {
                            commissionValueList.map((item, i) => {
                                return (<option key={`comm-${index}-${i}`} value={item}>{item}</option>);
                            })
                        }
                    </select>
                </td>
                <td className="text-center">
                    <a href="javacript:;" onClick={onRemove}><span className="oi oi-x text-danger"></span></a>
                </td>
            </tr>
        )
    }
}