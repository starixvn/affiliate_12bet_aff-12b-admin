import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { resetReducer } from '../actions';

export const resetAllProps = (PageComponent, name) => {
    const Item = class extends PureComponent {
        constructor(props) {
            super(props);
        }

        componentWillUnmount() {
            this.props.resetReducer(name);
        }

        render() {
            return (<PageComponent {...this.props} />);
        }
    }

    return connect(null, mapDispatchToProps)(Item);
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ resetReducer }, dispatch);
}