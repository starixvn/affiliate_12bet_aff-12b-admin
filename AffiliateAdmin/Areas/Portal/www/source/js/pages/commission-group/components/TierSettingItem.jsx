import React from "react";

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { INFINITY_SYMBOL, commissionValueList } from '../modules/constant';
import { formatDecimal } from '../../../modules/numberFormat';

const TierSettingItem = (props) => {
    const handleChange = (e) => {
        const target = e.target;
        const isCheckbox = target.type === 'checkbox';
        const value = isCheckbox ? target.checked : Number(target.value);
        const name = target.name;
        const { index, onChange } = props;

        const newItem = isCheckbox ? { 'infinityAm': value, 'infinityQm': value } : { [name]: value };

        onChange && onChange(index, newItem);
    }

    const { data, index, onRemove } = props;
    const isDisabled = data.infinityAm || data.infinityQm;

    return (
        <tr>
            <td className="text-center">{index + 1}</td>
            <td>
                <Input type="text" name="minAmount" id="minAmount" value={data.minAmount} onChange={handleChange} />
            </td>
            <td>
                <Form inline>
                    <Input type="text" name="maxAmount" id="maxAmount" className="form-max"
                        disabled={isDisabled} value={isDisabled ? INFINITY_SYMBOL : data.maxAmount} onChange={handleChange} />
                    <FormGroup check className="ml-sm-1">
                        <Label check>
                            <Input type="checkbox" name="infinityAm" checked={isDisabled} onChange={handleChange} />{INFINITY_SYMBOL}
                        </Label>
                    </FormGroup>
                </Form>
            </td>
            <td>
                <Input type="text" name="minQm" id="minQm" value={data.minQm} onChange={handleChange} />
            </td>
            <td>
                <Form inline>
                    <Input type="text" name="maxQm" id="maxQm" className="form-max"
                        disabled={isDisabled} value={isDisabled ? INFINITY_SYMBOL : data.maxQm} onChange={handleChange} />
                    <FormGroup check className="ml-sm-1">
                        <Label check>
                            <Input type="checkbox" name="infinityQm" checked={isDisabled} onChange={handleChange} />{INFINITY_SYMBOL}
                        </Label>
                    </FormGroup>
                </Form>
            </td>
            <td>
                <select className="form-control" name="commission" onChange={handleChange} value={formatDecimal(data.commission)}>
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

export default TierSettingItem;