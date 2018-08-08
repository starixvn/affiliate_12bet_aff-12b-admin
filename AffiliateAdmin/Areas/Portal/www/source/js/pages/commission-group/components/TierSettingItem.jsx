import React from "react";

import { Form, FormGroup, Label, Input } from 'reactstrap';
import { INFINITY_SYMBOL } from '../modules/constant';

const TierSettingItem = (props) => {
    const handleChange = (e) => {
        const target = e.target;
        const value = target.type === 'checkbox' ? target.checked : Number(target.value);
        const name = target.name;
        const { index, onChange } = props;

        const newItem = { [name]: value };
        onChange && onChange(index, newItem);
    }

    const { data, index, commissionList, onRemove } = props;
    const isDisabled = data.infinityAm || data.infinityQm;

    return (
        <tr>
            <td className="text-center">{index + 1}</td>
            <td>
                <Input type="text" name="minAm" id="minAm" value={data.minAm} onChange={handleChange} />
            </td>
            <td>
                <Form inline>
                    <Input type="text" name="maxAm" id="maxAm" className="form-max"
                        disabled={isDisabled} value={isDisabled ? INFINITY_SYMBOL : data.maxAm} onChange={handleChange} />
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
                <select className="form-control" name="commission" onChange={handleChange} value={data.commission.toString()}>
                    {
                        commissionList.map((item, i) => {
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