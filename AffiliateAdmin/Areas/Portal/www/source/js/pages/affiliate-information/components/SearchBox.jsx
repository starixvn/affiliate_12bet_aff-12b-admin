import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getLeftMenuInfo } from '../actions';

export class SearchBox extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            userName: '12BETAFF2233'
        };

        this.currentUserName = '';
    }

    componentDidMount() {
        const userName = this.state.userName;
        this.props.getLeftMenuInfo(userName);
    }

    handleChange = (event) => {
        this.setState({ userName: event.target.value.trim() });
    }

    handleClick = (event) => {
        event.preventDefault();
        const userName = this.state.userName;
        this.currentUserName = userName;
        this.props.getLeftMenuInfo(userName);
    }

    render() {
        const { userName } = this.state;
        const { hasData } = this.props;
        return (
            <table cellSpacing='0' cellPadding='0' width='98%'>
                <tbody>
                    <tr>
                        <td style={{ verticalAlign: 'top' }}>
                            <input className='text_f' style={{ width: 130 }} onChange={this.handleChange} value={userName} />
                            <a id='b_button' href='javacript:;' onClick={this.handleClick}>&nbsp;</a>
                        </td>
                    </tr>

                    <tr>
                        <td colSpan='2'>{!hasData && this.currentUserName !== '' && window.resource.noUser}</td>
                    </tr>
                </tbody>
            </table>
        )
    }
}

function mapStateToProps(state) {
    const { leftMenuData: { GeneralInfo: generalInfo } } = state;
    return { hasData: !!generalInfo };
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getLeftMenuInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchBox);