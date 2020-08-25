import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

import { setIsGeneral } from '../actions';
import LanguageComponent from './LanguageComponent';
import GeneralComponent from './GeneralComponent';

export const Content = (props) => {
    const { isGeneral } = props;

    return (
        < Container fluid={true} className="tab-container">
            <div className="tab-menu-container py-2">
                <ul class="nav">
                    <li className="nav-item">
                        <a href="javascript:;" className={`sub-nav-link tab-link ${isGeneral ? 'active' : ''}`}
                            onClick={() => props.setIsGeneral(true)}>
                            <span class="menu-text">General</span>
                        </a>
                    </li>
                    <li className="nav-item pl-3">
                        <a href="javascript:;" className={`sub-nav-link tab-link ${isGeneral ? '' : 'active'}`}
                            onClick={() => props.setIsGeneral(false)}>
                            <span class="menu-text">Language</span>
                        </a>
                    </li>
                </ul>
            </div>
            {
                isGeneral ? <GeneralComponent /> : <LanguageComponent />
            }
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