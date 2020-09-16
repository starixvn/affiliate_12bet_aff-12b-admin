import React from 'react';
import PropTypes from 'prop-types';

const Option = (props) => {
    const { itemData, onChange, id, isDisableOption } = props;

    const handleChange = (event) => {
        event.stopPropagation();

        const newItemData = {
            key: itemData.key,
            value: itemData.value,
            checked: !itemData.checked
        }

        onChange && onChange(newItemData);
    }

    return (
        <li className="multiple-select-item">
            <input
                id={id}
                type="checkbox"
                className="option-checkbox"
                checked={itemData.checked}
                onChange={handleChange}
                disabled={isDisableOption}
            />
            <label className="option-label" htmlFor={id}>
                {itemData.value}
            </label>
        </li>
    );
}

Option.propTypes = {
    itemData: PropTypes.object.isRequired,
    id: PropTypes.oneOfType([PropTypes.number, PropTypes.string]).isRequired,
    onChange: PropTypes.func,
    isDisableOption: PropTypes.bool
}

export default Option;