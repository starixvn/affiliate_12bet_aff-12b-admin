import React, { PureComponent, Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import LeftPanel from '../../left-panel/components/AppContainer';
import UserPanel from '../../user-panel/components/UserPanel';
import { setSelectedUserPanel } from '../../user-panel/actions';

const CustomerStatistics = React.lazy(() => 
    import(/* webpackChunkName: "customer-statistics" */ '../../customer-statistics/components/Content'));
export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { affId, selectedUserPanel } = this.props;
        return (
            <AppLayout>
                <div className='data-content m-2'>
                    <Row>
                        <div className="leftpanel">
                            <LeftPanel />
                        </div>
                        <div className="right-panel customer-statistics mx-2">
                            {
                                affId &&
                                <Suspense fallback={<div>Loading...</div>}>
                                    <CustomerStatistics />
                                </Suspense>
                            }
                            { 
                                affId && selectedUserPanel && 
                                <UserPanel selectedUserPanel={selectedUserPanel} onClickMenu={this.props.setSelectedUserPanel} /> 
                            }
                        </div>
                    </Row>
                </div>
            </AppLayout>
        );
    }
}

function mapStateToProps({ selectedUserPanel, affCommonInfo }) {
    return Object.assign({}, { selectedUserPanel, affId: affCommonInfo ? affCommonInfo.affId : null });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);