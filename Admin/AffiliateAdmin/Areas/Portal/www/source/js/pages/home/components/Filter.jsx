import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col, Button } from 'reactstrap';

import { countDownList, TenSeconds, SixtySeconds } from '../libs/constants';
import { getOnlineList, getPSPToday, getDepositInfo } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.counDownDropdown = React.createRef();

        this.state = {
            isChangingCountDown: false,
            selectedCountDown: SixtySeconds,
            countDown: SixtySeconds,
            time: '00:60'
        };
    }

    componentDidMount() {
        this._handleCountDown();
    }

    componentDidUpdate(prevProps, prevState) {
        const { selectedCountDown } = this.state;

        if (selectedCountDown !== prevState.selectedCountDown) {
            this.fetchData();
        }
    }

    _handleCountDown = () => {
        window.setInterval(() => {
            const { countDown, selectedCountDown } = this.state;

            if (countDown || selectedCountDown) {
                const time = countDown ? countDown - 1 : selectedCountDown - 1;
                let min = Math.floor(time / SixtySeconds);
                let sec = time % SixtySeconds;
                min = (min < TenSeconds ? "0" + min : min);
                sec = (sec < TenSeconds ? "0" + sec : sec);

                !countDown && this.fetchData();

                this.setState({ countDown: time, time: min + ":" + sec });
            }
        }, 1000);
    }

    fetchData = async () => {
        const { pagingOption } = this.props;

        await this.props.getOnlineList({ pageIndex: 1, pageSize: pagingOption.PageSize });
        await this.props.getPSPToday();
        await this.props.getDepositInfo();
    }

    _toggleChangingCountDown = (isChanging) => {
        this.setState({ isChangingCountDown: isChanging }, () => isChanging && this.counDownDropdown.current.focus());
    }

    _handleChangeCountDown = async (event) => {
        const value = event.target.value;

        this.setState({ selectedCountDown: value, countDown: value });

        await this.props.getOnlineList({ pageIndex: 1, pageSize: this.props.pagingOption.PageSize });
    }

    render() {
        const { isChangingCountDown, selectedCountDown, time } = this.state;
        return (
            <div className="filter-container">
                <Row>
                    <Col><span className="text-primary"><strong>Online List</strong></span></Col>
                    <Col className="text-right text-primary">
                        <select style={{ display: isChangingCountDown ? 'inline-block' : 'none' }} value={selectedCountDown}
                            onChange={this._handleChangeCountDown} onBlur={() => this._toggleChangingCountDown(false)} ref={this.counDownDropdown}>
                            {
                                Object.keys(countDownList).map(key => {
                                    return (<option value={key} key={`count-down-${key}`}>{countDownList[key]}</option>)
                                })
                            }
                        </select>
                        <Button color="link" className="home-btn-link" style={{ display: isChangingCountDown ? 'none' : 'inline-flex' }} onClick={() => this._toggleChangingCountDown(true)}>
                            {time} <span className="icontime"></span>
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
    return bindActionCreators({ getOnlineList, getPSPToday, getDepositInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);