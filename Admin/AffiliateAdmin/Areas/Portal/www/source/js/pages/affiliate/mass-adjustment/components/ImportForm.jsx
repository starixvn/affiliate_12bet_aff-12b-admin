import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label } from 'reactstrap';
import { setFile, setIsClickUpload } from '../actions';
import { InputFile } from '../../../common/components/InputFile';

export class ImportForm extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            files: []
        }
    }

    componentWillUnmount() {
        this.props.setFile(null);
    }

    componentDidUpdate(prevProps) {
        const { file } = this.props;

        if (!file && file !== prevProps.file) {
            this.setState({ files: [] });
        }
    }

    handleChangeFile = (event) => {
        let { files } = event.target;

        if (files && files.length > 0) {
            this.setState({ files });
        }
    }

    handleClickUpload = (hasFiles) => {
        const { files } = this.state;

        if (hasFiles) {
            this.props.setFile(files[0]);

            this.props.setIsClickUpload(true);
        }
    }

    render() {
        const { files } = this.state;
        const hasFiles = files && !!files.length;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="my-2 mr-sm-2">
                        <Label className="mr-sm-2">Import File</Label>
                        <InputFile className={`input-file ${hasFiles ? "text-dark" : "text-muted"}`}
                            btnName="Browse"
                            lblText="Select file..."
                            files={files}
                            handleOnChangeFiles={this.handleChangeFile}
                            accept=".xlsx, .xls" />
                    </FormGroup>

                    <Button className="my-2" disabled={!hasFiles} outline color="primary" type="button" onClick={() => this.handleClickUpload(hasFiles)}>
                        Upload
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ file }) {
    return Object.assign({}, { file });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setFile, setIsClickUpload }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ImportForm);