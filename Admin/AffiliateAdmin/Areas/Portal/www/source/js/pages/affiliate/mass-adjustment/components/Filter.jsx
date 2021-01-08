import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { setFile } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            file: null
        }
    }

    handleChangeFile = () => {

    }

    handleClickUpload = () => {

    }

    render() {
        const { file } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                     <FormGroup className="my-2 mr-sm-2">
                        <Label check>
                            <Label className="mr-sm-2">Import File</Label>
                            <InputFile files={file} handleOnChangeFiles={this.handleChangeFile}></InputFile>
                        </Label>
                    </FormGroup>

                    <Button className="my-2" outline color="primary" type="button" onClick={this.handleClickUpload}>
                        Upload
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({  }) {
    return Object.assign({}, { });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setFile }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);