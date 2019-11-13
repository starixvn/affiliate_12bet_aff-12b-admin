import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col, Button } from 'reactstrap';

import { countDownList } from '../libs/constants';
import { getOnlineList } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.counDownDropdown = React.createRef();

        this.state = {
            isChangingCountDown: false,
            countDown: 60
        }
    }

    _toggleChangingCountDown = (isChanging) => {
        this.setState({ isChangingCountDown: isChanging }, () => isChanging && this.counDownDropdown.current.focus());
    }

    _handleChangeCountDown = async (event) => {
        this.setState({ countDown: event.target.value });

        await this.props.getOnlineList({ pageIndex: 1, pageSize: this.props.pagingOption.PageSize });
    }

    render() {
        const { isChangingCountDown, countDown } = this.state;
        return (
            <div className="filter-container">
                <Row>
                    <Col><span className="text-primary">Online List</span></Col>
                    <Col className="text-right text-primary">
                        <select style={{ display: isChangingCountDown ? 'inline-block' : 'none' }} value={countDown}
                            onChange={this._handleChangeCountDown} onBlur={() => this._toggleChangingCountDown(false)} ref={this.counDownDropdown}>
                            {
                                Object.keys(countDownList).map(key => {
                                    return (<option value={key} key={`count-down-${key}`}>{countDownList[key]}</option>)
                                })
                            }
                        </select>
                        <Button color="link" className="left-panel-btn-link" style={{ display: isChangingCountDown ? 'none' : 'inline-block' }} onClick={() => this._toggleChangingCountDown(true)}>
                            {countDownList[countDown]}
                        </Button>
                    </Col>
                </Row>
            </div>
        );
    }
}

function mapStateToProps({ pagingOption }) {
    return Object.assign({}, { pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ getOnlineList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);