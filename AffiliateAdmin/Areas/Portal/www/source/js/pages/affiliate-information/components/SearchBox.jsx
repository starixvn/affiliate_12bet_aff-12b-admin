import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { getLeftMenuInfo } from '../actions';

export class SearchBox extends React.Component {
    constructor(props) {
        super(props);

        this.state = { userName: '12BETAFF2233' };
    }

    componentDidMount() {
        const userName = this.state.userName;
        this.props.getLeftMenuInfo(userName);
    }

    handleChange = (event) => {
        this.setState({ userName: event.target.value });
    }

    handleClick = (event) => {
        event.preventDefault();
        const userName = this.state.userName;
        this.props.getLeftMenuInfo(userName);
    }

    render() {
        return (
            <table cellSpacing='0' cellPadding='0' width='98%'>
                <tbody>
                    <tr>
                        <td style={{ verticalAlign: 'top' }}>
                            <input className='text_f' style={{ width: 130 }} onChange={this.handleChange} value={this.state.userName} />
                            <a id='b_button' href='javacript:;' onClick={this.handleClick}>&nbsp;</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getLeftMenuInfo }, dispatch);
}

export default connect(null, mapDispatchToProps)(SearchBox);