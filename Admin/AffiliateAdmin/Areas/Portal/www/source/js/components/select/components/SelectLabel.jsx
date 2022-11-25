import React from 'react';
import PropTypes from 'prop-types';

const SelectLabel = (props) => {
    const { id, selectedValue, isDisable } = props;

    return (
        <button type="button" className="select-button" id={`button-${id}`} disabled={isDisable}
            onClick={props.onToggle}>
            <span className="text-display" title={selectedValue}>{selectedValue}</span>
            <b className="caret"></b>
        </button>
    )
}

SelectLabel.propTypes = {
    id: PropTypes.string.isRequired,
    selectedValue: PropTypes.string.isRequired,
    isDisable: PropTypes.bool
}

export default SelectLabel;