import React, { Component } from 'react';
import { connect } from 'react-redux';

import SearchBox from './SearchBox';
import MainInfo from './MainInfo';

class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
        }
    }

    render() {
        console.log(this.props.leftMenuData);
        return (
            <div className='data-content'>
                <form>
                    <div id='left_pane'>
                        <table cellPadding='0' cellSpacing='0'>
                            <tbody>
                                <tr id='left_paneTop'></tr>
                                <tr>
                                    <td id='left_paneC'>
                                        <div id='contens_left'>
                                            <SearchBox />
                                            <MainInfo />
                                        </div>
                                        <div id="arrow_left" style={{ display: 'block' }}></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="left_panebottom">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </form>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { leftMenuData } = state;
    return { leftMenuData };
};

export default connect(mapStateToProps)(Content);