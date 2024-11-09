# Architectural Requirements

Architectural requirements will be documented via Architecture Decision Records (ADR). These records are stored in the `adr` directory and will have this format:

- Title: Brief description of the subject of the decision
- Number: The distinct number of the particular ADR
- Decision Date: The date the decision has been accepted
- State: One of: Draft, Active, Superseded, Dismissed
- Supersedes: The number of the ADR that this record supersedes, otherwise empty
- Superseded by: The number of the ADR that superseds this record, otherwise empty
- Body: The body of the ADR should contain these parts:
    - Description of the decision being made and the context that it is being made in
    - A list of options that could be selected
    - Identification of the selected option with a description of the reason it is selected

Each ADR will be contained in a single file and only one ADR will be contained in any given file. The file name should be in this format: `ADR-nnnn-{Title}`