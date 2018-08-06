import React from "react";

const INFINITY = "∞";

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
            <td><input type="text" className="input-tier" name="minAm" value={data.minAm} onChange={handleChange} /></td>
            <td>
                <input type="text" className="input-tier max" name="maxAm"
                    disabled={isDisabled} value={isDisabled ? INFINITY : data.maxAm} onChange={handleChange} />
                <input className="chk-infinity" type="checkbox" name="infinityAm"
                    checked={isDisabled} onChange={handleChange} />{INFINITY}
            </td>
            <td><input type="text" className="input-tier" name="minQm" value={data.minQm} onChange={handleChange} /></td>
            <td>
                <input type="text" className="input-tier max" name="maxQm"
                    disabled={isDisabled} value={isDisabled ? INFINITY : data.maxQm} onChange={handleChange} />
                <input className="chk-infinity" type="checkbox" name="infinityQm"
                    checked={isDisabled} onChange={handleChange} />{INFINITY}
            </td>
            <td>
                <select className="select-comm" name="commission" onChange={handleChange} value={data.commission.toString()}>
                    {
                        commissionList.map((item, i) => {
                            return (<option key={`comm-${index}-${i}`} value={item}>{item}</option>);
                        })
                    }
                </select>
            </td>
            <td className="text-center"><a href="javascript:;" onClick={onRemove}><span className="icdelete1"></span></a></td>
        </tr>
    )
}

export default TierSettingItem;