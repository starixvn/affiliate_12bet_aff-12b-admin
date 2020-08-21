import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

import { setIsGeneral } from '../actions';

export const Content = (props) => {
    const { isGeneral } = props;

    return (
        < Container fluid={true} className="permission-role-container">
            <div className="permission-role-sub-menu-container py-2">
                <ul class="nav">
                    <li className="nav-item">
                        <a href="javascript:;" className={`sub-nav-link ${isGeneral ? 'active' : ''}`}
                            onClick={() => props.setIsGeneral(true)}>
                            <span class="menu-text">General</span>
                        </a>
                    </li>
                    <li className="nav-item pl-3">
                        <a href="javascript:;" className={`sub-nav-link ${isGeneral ? '' : 'active'}`}
                            onClick={() => props.setIsGeneral(false)}>
                            <span class="menu-text">Language</span>
                        </a>
                    </li>
                </ul>
            </div>
            <h3>General Permission Role</h3>
        </Container>
    )
}

function mapStateToProps({ generalPermissionRole }) {
    const { isGeneral } = generalPermissionRole;
    return Object.assign({}, { isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setIsGeneral }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));