import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Form, FormGroup, Input, Label, Button } from 'reactstrap';

import { setParam, setSelectedMonth } from '../actions';
import { resource } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            selectedTrackingCode: ''
        }
    }

    componentDidMount() {
        this.handleClickSearch();
    }


    handleChange = (event) => {
        const { name, value } = event.target;
        this.setState({ [name]: value });
    }

    handleClickSearch = () => {
        const param = { trackingCode: this.state.selectedTrackingCode };

        this.props.setParam(param);
        this.props.setSelectedMonth(null);
    }

    render() {
        const { selectedTrackingCode } = this.state;
        const { trackingCodeList } = this.props;

        return trackingCodeList && (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-2">{resource.TrackingCode}</Label>
                        <Input type="select" name="selectedTrackingCode" value={selectedTrackingCode} onChange={this.handleChange}>
                            <option value="">{resource.All}</option>
                            {
                                trackingCodeList.map(trackingCode => {
                                    return (<option key={'trackingCode-' + trackingCode} value={trackingCode}>{trackingCode}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleClickSearch} >
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ userPanel }) {
    const { trackingCodeList } = userPanel;

    return Object.assign({}, {
        trackingCodeList: trackingCodeList || null
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setSelectedMonth }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);