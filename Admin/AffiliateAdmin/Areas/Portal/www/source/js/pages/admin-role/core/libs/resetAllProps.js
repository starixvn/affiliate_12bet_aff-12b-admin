import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { resetReducer } from '../actions';

export class Item extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentWillUnmount() {
        this.props.resetReducer(this.props.name);
    }

    render() {
        const { PageComponent, name, ...maintainProps } = this.props;

        return (<PageComponent {...maintainProps} />);
    }
}

export const resetAllProps = (PageComponent, name) => {

    function mapStateToProps(state) {
        return { PageComponent, name }
    }

    function mapDispatchToProps(dispatch) {
        return bindActionCreators({ resetReducer }, dispatch);
    }

    return connect(mapStateToProps, mapDispatchToProps)(Item);
}