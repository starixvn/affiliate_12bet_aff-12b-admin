import React from 'react';
import { connect } from 'react-redux';

export const Header = (props) => {
    return (
        <div className="header-container my-2">
            <strong>{props.userName || 'Create New User'}</strong>
        </div>
    )
}

function mapStateToProps({ userName }) {
    return Object.assign({}, { userName });
}

export default connect(mapStateToProps)(React.memo(Header));