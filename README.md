# STARshiP Pre-eclampsia Risk Calculator 
The STARshiP Calculator is an implementation of the pre-eclampsia screening algorithm described in [(Wright et al. 2020)](https://doi.org/10.1016/j.ajog.2019.11.1247) (sometimes referred to as the 'FMF algorithm'). The implementation was developed as part of the [STARshiP](https://www.nctu.ac.uk/our-research/randomised-trials/current-studies/starship.aspx) (Screen and Treat with Aspirin to Reduce Pre-eclampsia) study.

The code is an unfinished prototype, and has been released here for _reference only_, to assist other researchers and programmers in understanding how the algorithm works and in developing their own implementations. It should not be used in production or in a clinical setting, and is made available by the University of Nottingham on an "as is" basis without any warranty or acceptance of liability (see [licence details below](#licence)).

## How to run
The solution consists of two projects PeRiskCalc.Api and PeRiskCalc.Frontend. The frontend project is not part of the algorithm implementation and is intended to aid testing of the API only.

Run:
```bash
dotnet run --project PeRiskCalc.Api
dotnet run --project PeRiskCalc.Frontend
```

## Licence
Copyright (C) 2024 University of Nottingham

This software was written by Nicholas Russell, who exerts his moral right to be identified as the author of this work.
 
This software is the product of a non-commercial research project and is made available [[under a CC-BY 4.0 licence]](LICENSE) on an "as is" basis without any express or implied warranty of any kind. The University of Nottingham accepts no responsibility or liability for any losses arising as a result of your use of or reliance on this software. You use or rely on this software at your own risk. 