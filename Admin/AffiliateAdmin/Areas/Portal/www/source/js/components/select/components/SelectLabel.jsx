import React from 'react';
import PropTypes from 'prop-types';

const SelectLabel = (props) => {
    const { id, selectedText, isDisable } = props;

    return (
        <button type="button" className="select-button" disabled={isDisable}
            onClick={props.onToggle}>
            <span className="text-display" title={selectedText}>{selectedText}</span>
            <b className="caret"></b>
        </button>
    )
}

SelectLabel.propTypes = {
    selectedText: PropTypes.string.isRequired,
    isDisable: PropTypes.bool
}

export default SelectLabel;