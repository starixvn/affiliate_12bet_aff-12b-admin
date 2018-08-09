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

        const newItem = isCheckbox ? { 'InfinityAm': value, 'InfinityQm': value } : { [name]: value || 0 };

        onChange && onChange(index, newItem);
    }

    render() {
        const { data, index, onRemove, isDisabledInfinity } = this.props;
        const { MinAmount, MaxAmount, MinQm, MaxQm, Commission, InfinityAm, InfinityQm } = data;
        const isDisabled = InfinityAm || InfinityQm;
        const isDisabledMinAmount = index > 0;
        
        return (
            <tr>
                <td className="text-center">{index + 1}</td>
                <td>
                    <Input type="text" name="MinAmount" id="MinAmount" disabled={isDisabledMinAmount}
                        value={format(MinAmount)} onChange={this.handleChange} />
                </td>
                <td>
                    <Form inline>
                        <Input type="text" name="MaxAmount" id="MaxAmount" className="form-max" disabled={isDisabled}
                            value={isDisabled ? INFINITY_SYMBOL : format(MaxAmount)} onChange={this.handleChange} />
                        <FormGroup check className="ml-sm-1">
                            <Label check>
                                <Input type="checkbox" name="InfinityAm" checked={isDisabled}
                                    onChange={this.handleChange} disabled={isDisabledInfinity} />{INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                    </Form>
                </td>
                <td>
                    <Input type="text" name="MinQm" id="MinQm" value={format(MinQm)} onChange={this.handleChange} />
                </td>
                <td>
                    <Form inline>
                        <Input type="text" name="MaxQm" id="MaxQm" className="form-max" disabled={isDisabled}
                            value={isDisabled ? INFINITY_SYMBOL : format(MaxQm)} onChange={this.handleChange} />
                        <FormGroup check className="ml-sm-1">
                            <Label check>
                                <Input type="checkbox" name="InfinityQm" checked={isDisabled}
                                    onChange={this.handleChange} disabled={isDisabledInfinity} />{INFINITY_SYMBOL}
                            </Label>
                        </FormGroup>
                    </Form>
                </td>
                <td>
                    <select className="form-control" name="Commission" 
                        onChange={this.handleChange} value={formatDecimal(Commission)}>
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