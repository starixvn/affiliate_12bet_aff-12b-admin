import React, { Fragment, PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import { setSortedParam } from '../actions';
import { SortByIdList } from '../libs/constants';

export class AppContainer extends PureComponent {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.setSortedParam({ sortById: this.props.hasTurnOverPermission ? SortByIdList.Turnover : SortByIdList.Winloss, isSortAsc: false });
    }

    render() {
        return (
            <AppLayout>
                {
                    this.props.sortedParam && (
                        <Fragment>
                            <Filter />
                            <Content />
                        </Fragment>
                    )
                }
            </AppLayout>
        );
    }
}
function mapStateToProps({ hasTurnOverPermission, playerDetail }) {
    const { sortedParam } = playerDetail;

    return Object.assign({}, { sortedParam, hasTurnOverPermission });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSortedParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);