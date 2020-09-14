import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

import LanguageComponent from './LanguageComponent';
import GeneralComponent from './GeneralComponent';
import { setIsGeneral, getRoleList, setGridData } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

    }

    componentDidMount() {
        this.props.getRoleList();
    }

    _handleChangeTab = (isGeneral) => {
        this.props.setGridData(null);
        this.props.setIsGeneral(isGeneral);
    }

    render() {
        const { isGeneral } = this.props;

        return (
            <div className="data-content">
                < Container fluid={true} className="tab-container">
                    <div className="tab-menu-container py-2">
                        <ul class="nav">
                            <li className="nav-item">
                                <a href="javascript:;" className={`sub-nav-link tab-link ${isGeneral ? 'active' : ''}`}
                                    onClick={() => this._handleChangeTab(true)}>
                                    <span class="menu-text">General</span>
                                </a>
                            </li>
                            <li className="nav-item pl-3">
                                <a href="javascript:;" className={`sub-nav-link tab-link ${isGeneral ? '' : 'active'}`}
                                    onClick={() => this._handleChangeTab(false)}>
                                    <span class="menu-text">Language</span>
                                </a>
                            </li>
                        </ul>
                    </div>
                    {
                        isGeneral ? <GeneralComponent /> : <LanguageComponent />
                    }
                </Container>
            </div>
        )
    }
}

function mapStateToProps({ rolePermission }) {
    const { isGeneral } = rolePermission;
    return Object.assign({}, { isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setIsGeneral, getRoleList, setGridData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));