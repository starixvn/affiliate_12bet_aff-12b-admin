import React, { PureComponent, Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row } from 'reactstrap';

import AppLayout from '../../../layouts/AppLayout';
import { LeftPanel } from '../modules/reduxComponentFactory';
import UserPanel from './UserPanel';
import { setSelectedUserPanel, loadUserPanelState } from '../left-panel/actions/commonAction';

const CustomerStatistics = React.lazy(() => import(/* webpackChunkName: "customer-statistics" */ '../customer-statistics/components/Content'));
export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleClickMenu = (selectedMenuKey) => {
        this.props.setSelectedUserPanel(selectedMenuKey);
        loadUserPanelState(selectedMenuKey)
    }

    render() {
        const { affCommonInfo, selectedUserPanel } = this.props;
        return (
            <AppLayout>
                <div className='data-content m-2'>
                    <Row>
                        <div className="leftpanel">
                            <LeftPanel />
                        </div>
                        <div className="right-panel customer-statistics mx-2">
                            {
                                affCommonInfo &&
                                <Suspense fallback={<div>Loading...</div>}>
                                    <CustomerStatistics />
                                </Suspense>
                            }
                            { 
                                affCommonInfo && selectedUserPanel && 
                                <UserPanel selectedUserPanel={selectedUserPanel} onClickMenu={this.handleClickMenu} /> 
                            }
                        </div>
                    </Row>
                </div>
            </AppLayout>
        );
    }
}

function mapStateToProps({ selectedUserPanel, affCommonInfo }) {
    return Object.assign({}, { selectedUserPanel, affCommonInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);