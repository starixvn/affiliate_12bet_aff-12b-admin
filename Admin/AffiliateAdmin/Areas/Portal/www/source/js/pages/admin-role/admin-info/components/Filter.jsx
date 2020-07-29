import React from 'react';
import { connect } from 'react-redux';

export const Filter = (props) => {
    return (
        <div className="filter-container mt-2">
            <strong>{props.userName || 'Create New User'}</strong>
        </div>
    )
}

function mapStateToProps({ userName }) {
    return Object.assign({}, { userName });
}

export default connect(mapStateToProps)(React.memo(Filter));