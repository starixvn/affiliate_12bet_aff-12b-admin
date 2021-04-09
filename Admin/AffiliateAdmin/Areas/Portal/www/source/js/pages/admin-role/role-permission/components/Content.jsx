import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container } from 'reactstrap';

import LanguageComponent from './LanguageComponent';
import GeneralComponent from './GeneralComponent';
import MyLink from '../../../common/components/MyLink';
import { setIsGeneral, getRoleList, getMarketList } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

    }

    componentDidMount() {
        this.props.getRoleList();
        this.props.getMarketList();
    }

    _handleChangeTab = (isGeneral) => {
        this.props.setIsGeneral(isGeneral);
    }

    render() {
        const { isGeneral } = this.props;

        return (
            <div className="data-content">
                < Container fluid={true} className="tab-container">
                    <div className="tab-menu-container py-2">
                        <ul className="nav">
                            <li className="nav-item">
                                <MyLink className={`sub-nav-link tab-link ${isGeneral ? 'active' : ''}`}
                                    onClick={() => this._handleChangeTab(true)}>
                                    <span className="menu-text">General</span>
                                </MyLink>
                            </li>
                            <li className="nav-item pl-3">
                                <MyLink className={`sub-nav-link tab-link ${isGeneral ? '' : 'active'}`}
                                    onClick={() => this._handleChangeTab(false)}>
                                    <span className="menu-text">Language</span>
                                </MyLink>
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

function mapStateToProps({ isGeneral }) {
    return Object.assign({}, { isGeneral });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setIsGeneral, getRoleList, getMarketList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);