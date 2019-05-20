import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { resetUserPanel } from '../actions';

export const withUserPanel = (PageComponent, name) => {
    const UserPanelItem = class extends PureComponent {
        constructor(props) {
            super(props);
        }

        componentWillUnmount() {
            this.props.resetUserPanel(name);
        }
        
        render() {
            return (<PageComponent {...this.props } />);
        }
    }

    return connect(null, mapDispatchToProps)(UserPanelItem);
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ resetUserPanel }, dispatch);
}