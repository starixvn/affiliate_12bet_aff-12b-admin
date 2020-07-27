import React, { Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import CommonGridData from './CommonGridData';
import { setIsGeneral } from '../actions';

export const Content = (props) => {
    const { isGeneral } = props;

    return (
        <Fragment>
            <div className="permission-role-menu-container">
                <span class="icon-permission-role"></span>
                <ul class="d-inline-block menu-list">
                    <li className="up-item">
                        <a href="javascript:;" className={`upitem-link ${isGeneral ? 'active' : ''}`}
                            onClick={() => props.setIsGeneral(true)}>
                            <span class="menu-text">General</span>
                        </a>
                    </li>
                    <li className="up-item">
                        <a href="javascript:;" className={`upitem-link ${isGeneral ? '' : 'active'}`}
                            onClick={() => props.setIsGeneral(false)}>
                            <span class="menu-text">Language</span>
                        </a>
                    </li>
                </ul>
            </div>
            <CommonGridData />
        </Fragment>
    )
}

function mapStateToProps({ isGeneral }) {
    return Object.assign({}, { isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setIsGeneral }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));